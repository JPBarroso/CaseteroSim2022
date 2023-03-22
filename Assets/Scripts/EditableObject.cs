using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableObject : MonoBehaviour
{

    private GameObject editPanel;
    private PlaceableObjects placeableObjects;
    [SerializeField] private GameObject objTop;

    private Vector3 lastPosition;
    private Quaternion triple;

    [SerializeField] private MeshRenderer[] meshRenderer;
    public Material[] originalMaterials;
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
        
        if (ES3.FileExists(SaveAndLoadManager.FileName) && placeableObjects.isAlreadyBougth)
        {
            originalMaterials = ES3.Load<Material[]>(this.gameObject.name, SaveAndLoadManager.FileName);
        }

        if (!placeableObjects.isAlreadyBougth)
        {
            ChangeMaterialWhenEdit();
        }
    }

    private void OnMouseDown()
    {
        //Activamos la UI de edicion(el panel)
        ActivateEditableUI();
        if (GameModeController.Instance.actualMode == GameModeController.GameActualMode.EDIT)
        {
            BuildingSystem.Instance.objToPlace = this.gameObject.GetComponent<PlaceableObjects>();
        }

    }

    private void TouchAndEdit()
    {
        //Activamos la UI de edicion(el panel)
        ActivateEditableUI();
        BuildingSystem.Instance.objToPlace = this.gameObject.GetComponent<PlaceableObjects>();
    }

    private void ActivateEditableUI()
    {
        if (GameModeController.Instance.actualMode == GameModeController.GameActualMode.WAIT)
        {
            GameModeController.Instance.actualMode = GameModeController.GameActualMode.EDIT;
        }
        
        if (GameModeController.Instance.actualMode == GameModeController.GameActualMode.EDIT)
        {
            if (placeableObjects.isAlreadyBougth)//Solo activamos esto si al clicar el objeto ya está comprado
            {
                ActivateUIComponent activate = FindObjectOfType<ActivateUIComponent>();
                activate.ActivateThisUIComponent();
                ActivateObjectWhenEdit();
            }
        }
    }

    private void ActivateObjectWhenEdit()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Pick");
        
        if (temp != null)
        {
            Destroy(temp);
        }

        Vector3 add = new Vector3(0f, 1.25f, 0f);
        Vector3 pos = this.transform.position + add;
        GameObject pick = Instantiate(objTop, pos, Quaternion.identity);
        pick.transform.parent = gameObject.transform;
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

    public void SaveMaterials()
    {
        ES3.Save(this.gameObject.name, originalMaterials, SaveAndLoadManager.FileName);
    }

    public void LoadMaterials()
    {
        if (ES3.FileExists(SaveAndLoadManager.FileName))
        {
            originalMaterials = ES3.Load<Material[]>(this.gameObject.name, SaveAndLoadManager.FileName);
            meshRenderer = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material = originalMaterials[i];
            }
        }
    }

    public void SaveLastPositionBeforeEdite()
    {
        var transform1 = this.transform;
        lastPosition = transform1.position;
        triple = transform1.rotation;
    }

    public void ReturnToLastPositionTest()
    {
        Debug.Log("return to" + lastPosition + triple);
        var transform1 = this.transform;
        transform1.position = lastPosition;
        transform1.rotation = triple;
    }
}
