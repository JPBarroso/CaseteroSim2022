using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjects : MonoBehaviour
{

    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set; }
    private Vector3[] vertices;

    private GameObject plane;
    private Transform planeTransform;

    private void Awake()
    {
        plane = GameObject.FindGameObjectWithTag("Ground");
        planeTransform = plane.transform;
    }

    private void Start()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeInCells();
        
        RepositionObj();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0,45,0));
        Size = new Vector3Int(Size.y, Size.x, 1);

        Vector3[] verticesTemp = new Vector3[vertices.Length];
        for (int i = 0; i < verticesTemp.Length; i++)
        {
            verticesTemp[i] = vertices[(i + 1) % vertices.Length];
        }

        vertices = verticesTemp;
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

    private void RepositionObj()
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

        float boxSize = boxCollider.size.y;
        float boxCenter = boxCollider.center.y;
        float testValueY = (boxSize + boxCenter) / 2;
        float testPlaneValueDistance = testValueY - plane.transform.position.y;
        float newPositionTest = plane.transform.position.y + boxSize / 2;

        transform.position = new Vector3(transform.position.x, newPositionTest, transform.position.z);
        
        Debug.Log("El tamaño de la y del collider es" + boxCollider.size.y);
        Debug.Log("El centro de la y del colider es" + boxCollider.center.y);
        Debug.Log("El test da" + testValueY);
        Debug.Log("El valor de la distancia del plano y lo otro es" + testPlaneValueDistance);
        Debug.Log("La distancia del centro hasta el plano es" + TestWithRay().y);
    }

    private Vector3 TestWithRay()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out  RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        Placed = true;
        
        //Aqui colocamos. Podemos suscribir aqui distintos eventos para descontar dinero o lo que necesitemos
    }

    private void ChangeAlphaObj(float value)//Cambiar el alfa, mas adelante
    {
        
    }
    
}
