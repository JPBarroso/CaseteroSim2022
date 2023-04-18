using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Este script está en todos los objetos y se encarga de darle su comportamiento además de ser quien se comunica con otros componentes
public class PlaceableObjects : MonoBehaviour
{

    public bool placed;
    public bool isAlreadyBougth;
    public Vector3Int Size { get; private set; }
    private Vector3[] vertices;
    private Furniture furniture;
    
    public enum MODE//Modos en los que puede estar el objeto
    {
        Buymode,
        Editmode,
        Putmode
    }
    public MODE furnitureMode;

    private void Start()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeInCells();
    }

    private void Update()
    {
        CheckForGround();
    }

    public void Rotate(float value)
    {
        transform.Rotate(new Vector3(0,value,0));
        Size = new Vector3Int(Size.y, Size.x, 1);

        Vector3[] verticesTemp = new Vector3[vertices.Length];
        for (int i = 0; i < verticesTemp.Length; i++)
        {
            verticesTemp[i] = vertices[(i + 1) % vertices.Length];
        }

        vertices = verticesTemp;
    }

    public void ChangeYPositionOfObject(GameObject obj, float valueToAdd)
    {
        Vector3 objPosition = obj.transform.position;
        obj.transform.position = new Vector3(objPosition.x, objPosition.y + valueToAdd,
            objPosition.z);
    }

    //Obtenemos las esquinas del box colider para generar su posicion
    private void GetColliderVertexPositionLocal()
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        vertices = new Vector3[4];
        vertices[0] = boxCollider.center +
                      new Vector3(-boxCollider.size.x, -boxCollider.size.y, -boxCollider.size.z) * 0.25f;
        vertices[1] = boxCollider.center +
                      new Vector3(boxCollider.size.x, -boxCollider.size.y, -boxCollider.size.z) * 0.25f;
        vertices[2] = boxCollider.center +
                      new Vector3(boxCollider.size.x, -boxCollider.size.y, boxCollider.size.z) * 0.25f;
        vertices[3] = boxCollider.center +
                      new Vector3(-boxCollider.size.x, -boxCollider.size.y, boxCollider.size.z) * 0.25f;
    }

    //Calculamos el tamaño necesario por celda
    private void CalculateSizeInCells()
    {
        Vector3Int[] verticesTemp = new Vector3Int[vertices.Length];

        for (int i = 0; i < verticesTemp.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            verticesTemp[i] = BuildingSystem.Instance.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Math.Abs((verticesTemp[0] - verticesTemp[1]).x), Math.Abs((verticesTemp[0] - verticesTemp[3]).y), 1);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }

    public bool isTouchingGround;
    private void CheckForGround()
    {
        Vector3 pos = this.transform.position;
        isTouchingGround = Physics.Raycast(new Vector3(pos.x, pos.y + 1, pos.z), Vector3.down, 5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(pos, Vector3.down * 2, Color.green);
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        placed = true;
        isAlreadyBougth = true;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        Transform[] allObj = this.gameObject.GetComponentsInChildren<Transform>();
        foreach (var g in allObj)
        {
            g.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        //Aqui colocamos. Podemos suscribir aqui distintos eventos para descontar dinero o lo que necesitemos
    }

    public void SavePlacedBooleans()
    {
        ES3.Save("isAlreadyBougth",isAlreadyBougth, SaveAndLoadManager.FileName);
        ES3.Save("isAlreadyPlaced",placed, SaveAndLoadManager.FileName);
    }

}
