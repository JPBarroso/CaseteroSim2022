using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadManager : MonoBehaviour
{

    public List<GameObject> prefabsToSaveList = new List<GameObject>();
    public static string FileName => "SaveCasetaFile" + ".es3";

    public void AddGameObjToList(GameObject prefabToAdd)
    {
        prefabsToSaveList.Add(prefabToAdd);
    }

    public void DeleteFromList(GameObject prefabToAdd)
    {
        prefabsToSaveList.Remove(prefabToAdd);
    }

    private void LoadPlaceableBooleans()
    {
        for (int i = 0; i < prefabsToSaveList.Count; i++)
        {
            PlaceableObjects p = prefabsToSaveList[i].GetComponent<PlaceableObjects>();
            p.placed = ES3.Load<bool>("isAlreadyBougth", SaveAndLoadManager.FileName);
            p.isAlreadyBougth = ES3.Load<bool>("isAlreadyPlaced", SaveAndLoadManager.FileName);
            Debug.Log("count");
        }
    }

    private void LoadMateriales()
    {
        for (int i = 0; i < prefabsToSaveList.Count; i++)
        {
            EditableObject e = prefabsToSaveList[i].GetComponent<EditableObject>();
            e.originalMaterials = ES3.Load<Material[]>("OriginalMat", FileName);
        }

    }

    public void SaveGame()
    {
        Debug.Log("Estoy guardando");
        if (prefabsToSaveList.Count > 0)
        {
            Debug.Log("guardado con exito");
            ES3.Save("furnituresInstance", prefabsToSaveList, FileName);
        }
        
    }

    public void LoadGame()
    {
        Debug.Log("Intento Cargar");
        if (ES3.FileExists(FileName))
        {
            Debug.Log("Estoy cargando");
            prefabsToSaveList = ES3.Load("furnituresInstance", FileName, new List<GameObject>());
            LoadPlaceableBooleans();
            LoadMateriales();
        }
        
    }

    public void DeleteGame()
    {
        Debug.Log("Borrando");
        ES3.DeleteFile(FileName);
    }
    
}
