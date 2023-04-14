using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrag : MonoBehaviour
{

    //Estos métodos habrá que investigar como usarlos con el touch del móvil
    private Vector3 offset;
    private bool isClicked;
    private bool outOfMouse;

    private PlaceableObjects placeableObjects;
    private EditableObject editableObject;

    private void Awake()
    {
        placeableObjects = GetComponent<PlaceableObjects>();
        editableObject = GetComponent<EditableObject>();
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
    /*private void OnMouseDown()
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
        
    }*/
    #endif

    
    private void TouchItemsPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            int id = touch.fingerId;

            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(id))
            {
                Debug.Log("touching");
                if (placeableObjects.isTouchingGround)
                {
                    offset = transform.position - BuildingSystem.Instance.GetTouchPosition();
                    outOfMouse = true;
                }
            }

            if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(id))
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
            editableObject.ChangeMaterialWhenCantPlace();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Furniture") || other.gameObject.CompareTag("Config"))
        {
            canBePlaced = true;
            editableObject.ChangeMaterialWhenEdit();
        }
    }
}
