using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{

    //Estos métodos habrá que investigar como usarlos con el touch del móvil
    private Vector3 offset;
    private bool isClicked;

    private PlaceableObjects placeableObjects;

    private void Awake()
    {
        placeableObjects = GetComponent<PlaceableObjects>();
    }

    private void Start()
    {
        canBePlaced = true;
    }

    private void Update()
    {
        if (isClicked)
        {
            
        }
        //Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        //transform.position = BuildingSystem.Instance.SnapCoordinateToGrid(pos);
    }

    private bool outOfMouse;
    
    private void OnMouseDown()
    {
        if (placeableObjects.isTouchingGround)
        {
            offset = transform.position - BuildingSystem.Instance.GetMouseWorldPosition();
            outOfMouse = true;
        }
    }

    private void OnMouseDrag()
    {
        if (placeableObjects.isTouchingGround && outOfMouse)
        {
            Vector3 pos = BuildingSystem.Instance.GetMouseWorldPosition() + offset;
            transform.position = BuildingSystem.Instance.SnapCoordinateToGrid(pos);
        }
        else
        {
            outOfMouse = false;
            transform.position = BuildingSystem.Instance.SnapCoordinateToGrid(Vector3.zero);
        }
        
    }

    public bool canBePlaced = true;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Furniture") || other.gameObject.CompareTag("Config"))
        {
            canBePlaced = false;
            Debug.Log("Esta tocando");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Furniture") || other.gameObject.CompareTag("Config"))
        {
            canBePlaced = true;
        }
    }
}
