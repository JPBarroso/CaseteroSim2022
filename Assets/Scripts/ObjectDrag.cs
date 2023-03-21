using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{

    //Estos métodos habrá que investigar como usarlos con el touch del móvil
    private Vector3 offset;
    private bool isClicked;
    private bool outOfMouse;

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
        //Voy a testear los touch del movil
        TouchItemsPosition();
    }
    
    
    #if UNITY_EDITOR || UNITY_EDITOR_WIN
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
    #endif

    
    private void TouchItemsPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("touching");
                if (placeableObjects.isTouchingGround)
                {
                    offset = transform.position - BuildingSystem.Instance.GetTouchPosition();
                    outOfMouse = true;
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("moving");
                if (placeableObjects.isTouchingGround && outOfMouse)
                {
                    Vector3 pos = BuildingSystem.Instance.GetTouchPosition() + offset;         
                    transform.position = BuildingSystem.Instance.SnapCoordinateToGrid(pos);     
                }
                else
                {
                    outOfMouse = false;
                    transform.position = BuildingSystem.Instance.SnapCoordinateToGrid(Vector3.zero);
                }
            }
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
