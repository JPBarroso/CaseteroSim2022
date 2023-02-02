using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjects : MonoBehaviour
{

    public bool placed;
    public Vector3Int Size { get; private set; }
    private Vector3[] vertices;

    private Furniture furniture;
    public bool isAlreadyBougth;
    
    public enum MODE
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

        isAlreadyBougth = false;
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

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        placed = true;
        isAlreadyBougth = true;

        //Aqui colocamos. Podemos suscribir aqui distintos eventos para descontar dinero o lo que necesitemos
    }

    private void ChangeAlphaObj(float value)//Cambiar el alfa, mas adelante
    {
        
    }
    
}
