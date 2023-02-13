using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadManager : MonoBehaviour
{

    public List<GameObject> prefabsToSaveList = new List<GameObject>();


    public void AddGameObjToList(GameObject prefabToAdd)
    {
        prefabsToSaveList.Add(prefabToAdd);
    }

    public void DeleteFromList(GameObject prefabToAdd)
    {
        prefabsToSaveList.Remove(prefabToAdd);
    }

    public void SaveGame()
    {
        ES3.Save("furnituresInstance", prefabsToSaveList);
    }

    public void LoadGame()
    {
        prefabsToSaveList = ES3.Load("furnituresInstance", new List<GameObject>());
    }
    
    
}
