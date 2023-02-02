using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableObject : MonoBehaviour
{

    private GameObject editPanel;

    private void OnMouseDown()
    {
        if (BuildingSystem.Instance.objectHasBeenPurchase)
        {
            Debug.Log("puedes editar el objeto");
            ActivateUIComponent activate = FindObjectOfType<ActivateUIComponent>();
            activate.ActivateThisUIComponent();
        }
        else
        {
            return;
        }
    }

    private GameObject SetThisFurnitureEditable()
    {
        return this.gameObject;
    }
}
