using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableObject : MonoBehaviour
{

    private GameObject editPanel;
    private PlaceableObjects placeableObjects;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] originalMaterials;
    [SerializeField] private Material newmaterial;

    private void Start()
    {
        placeableObjects = GetComponent<PlaceableObjects>();

        meshRenderer = GetComponentInChildren<MeshRenderer>(); 
        originalMaterials = meshRenderer.materials;
        
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
        Material[] mats = meshRenderer.materials;
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            mats[i] = newmaterial;
        }
        meshRenderer.materials = mats;
    }

    public void ReturnMaterialsWhenFinishEdit()
    {
        meshRenderer.materials = originalMaterials;
    }
}
