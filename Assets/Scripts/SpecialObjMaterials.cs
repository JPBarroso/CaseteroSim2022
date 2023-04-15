using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialObjMaterials : MonoBehaviour
{

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material[] allMaterials;
    [SerializeField] private Material[] originalMaterials;

    [SerializeField] private Material newMaterialBlue;
    [SerializeField] private Material newMaterialRed;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        allMaterials = mesh.materials;
        originalMaterials = allMaterials;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeMaterialToOriginals();
        }
    }

    public void ChangeMaterialsToBlue()
    {
        Material[] nuevosMateriales = new Material[allMaterials.Length];
        
        for (int i = 0; i < allMaterials.Length; i++)
        {
            nuevosMateriales[i] = newMaterialBlue;
        }
        
        mesh.materials = nuevosMateriales;
    }

    public void ChangeMaterialsToRed()
    {
        Material[] nuevosMateriales = new Material[allMaterials.Length];
        
        for (int i = 0; i < allMaterials.Length; i++)
        {
            nuevosMateriales[i] = newMaterialRed;
        }
        
        mesh.materials = nuevosMateriales;
    }

    public void ChangeMaterialToOriginals()
    {
        Debug.Log("Cambio material normal especial");
        mesh.materials  = originalMaterials;
    }

    public void SaveThisSpecialsAndConcreteMaterials()
    {
        ES3.Save(this.gameObject.name, originalMaterials, SaveAndLoadManager.FileName);
    }

    public void LoadThisSpecialsMaterials()
    {
        if (ES3.FileExists(SaveAndLoadManager.FileName))
        {
            originalMaterials = ES3.Load<Material[]>(this.gameObject.name, SaveAndLoadManager.FileName);
            mesh.materials = originalMaterials;
            mesh.materials = allMaterials;
        }
    }
    
    //Me quiero morir no se que ocurre
    private IEnumerator WaitAFrameToPutOriginalMaterials()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Lacorrutinafunciona");
        ChangeMaterialToOriginals();
    }

    public void StartMaterialsCorroutine()
    {
        StartCoroutine(WaitAFrameToPutOriginalMaterials());
    }


}
