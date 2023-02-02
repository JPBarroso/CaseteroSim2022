using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableObject : MonoBehaviour
{

    private GameObject editPanel;
    private PlaceableObjects placeableObjects;

    private void Start()
    {
        placeableObjects = GetComponent<PlaceableObjects>();
    }

    private void OnMouseDown()
    {
        ActivateEditableUI();
        BuildingSystem.Instance.objToPlace = this.gameObject.GetComponent<PlaceableObjects>();
    }

    private void ActivateEditableUI()
    {
        if (placeableObjects.isAlreadyBougth)
        {
            Debug.Log("puedes editar el objeto");
            ActivateUIComponent activate = FindObjectOfType<ActivateUIComponent>();
            activate.ActivateThisUIComponent();
        }
    }
}
