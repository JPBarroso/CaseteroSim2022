using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadManager : MonoBehaviour
{

    public List<GameObject> prefabsToSaveList = new List<GameObject>();
    public static string FileName => "SaveCasetaFile" + ".es3";

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    
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
        Debug.Log("Estoy guardando");
        if (prefabsToSaveList.Count > 0)
        {
            Debug.Log("guardado con exito");
            ES3.Save("furnituresInstance", prefabsToSaveList, FileName);
            
            //Guardado de materiales
            for (int i = 0; i < prefabsToSaveList.Count; i++)
            {
                PlaceableObjects p = prefabsToSaveList[i].GetComponent<PlaceableObjects>();
                p.SavePlacedBooleans();
            }
        }

    }

    private void LoadPlaceableBooleans()
    {
        for (int i = 0; i < prefabsToSaveList.Count; i++)
        {
            PlaceableObjects p = prefabsToSaveList[i].GetComponent<PlaceableObjects>();
            p.placed = ES3.Load<bool>("isAlreadyBougth", SaveAndLoadManager.FileName);
            p.isAlreadyBougth = ES3.Load<bool>("isAlreadyPlaced", SaveAndLoadManager.FileName);
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
        }
    }

    public void DeleteGame()
    {
        Debug.Log("Borrando");
        ES3.DeleteFile(FileName);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }
    
    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        //LoadGame();
    }
    
    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as t$$anonymous$$s script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    
}
