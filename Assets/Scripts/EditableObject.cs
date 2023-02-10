using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableObject : MonoBehaviour
{

    private GameObject editPanel;
    private PlaceableObjects placeableObjects;

    [SerializeField] private MeshRenderer[] meshRenderer;
    [SerializeField] private Material[] originalMaterials;
    [SerializeField] private Material newmaterial;

    private void Start()
    {
        placeableObjects = GetComponent<PlaceableObjects>();

        meshRenderer = GetComponentsInChildren<MeshRenderer>();
        originalMaterials = new Material[meshRenderer.Length];
        
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            originalMaterials[i] = meshRenderer[i].material;
        }
        
        Debug.Log("editable" + originalMaterials.Length);
        ChangeMaterialWhenEdit();
    }

    private void OnMouseDown()
    {
        //Activamos la UI de edicion(el panel)
        ActivateEditableUI();
        BuildingSystem.Instance.objToPlace = this.gameObject.GetComponent<PlaceableObjects>();
    }

    private void ActivateEditableUI()
    {
        if (placeableObjects.isAlreadyBougth)//Solo activamos esto si al clicar el objeto ya está comprado
        {
            Debug.Log("puedes editar el objeto");
            ActivateUIComponent activate = FindObjectOfType<ActivateUIComponent>();
            activate.ActivateThisUIComponent();
        }
    }

    public void ChangeMaterialWhenEdit()
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].material = newmaterial;
        }
    }

    public void ReturnMaterialsWhenFinishEdit()
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].material = originalMaterials[i];
        }
        
    }
    
    
    
}
