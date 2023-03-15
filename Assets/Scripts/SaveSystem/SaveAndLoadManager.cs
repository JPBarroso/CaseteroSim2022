using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> prefabsToSaveList = new List<GameObject>();
    [SerializeField] private GameObject prefabConfigSave;
    public static string FileName => "SaveCasetaFile" + ".es3";
    [SerializeField] private Shop shopAvailable;
    [SerializeField] private ShopSystem shopSystem;
    [SerializeField] private PermanentsObjController pObjController;

    private GameObject[] allConfigInScene;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void Awake()
    {
        if (ES3.FileExists(FileName))
        {
            LoadGame();
        }
    }

    public void AddGameObjToList(GameObject prefabToAdd)
    {
        prefabsToSaveList.Add(prefabToAdd);
    }

    public void DeleteFromList(GameObject prefabToAdd)
    {
        prefabsToSaveList.Remove(prefabToAdd);
    }

    public void AddToPlaceReference(GameObject placedObj)
    {
        prefabConfigSave = placedObj;
    }
    
    public void SaveGame()
    {
        SavePrefabs();
        SaveMoney();
        SaveConfig();
    }

    private void SavePrefabs()
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
                EditableObject e = prefabsToSaveList[i].GetComponent<EditableObject>();
                e.SaveMaterials();
                p.SavePlacedBooleans();
            }
        }
    }

    private void SaveMoney()
    {
        ES3.Save("Money", shopAvailable.moneyAvailable, FileName);
    }

    private void SaveConfig()
    {
        ES3.Save("Config", prefabConfigSave, FileName);
    }

    private void LoadPlaceableBooleans()
    {
        for (int i = 0; i < prefabsToSaveList.Count; i++)
        {
            PlaceableObjects p = prefabsToSaveList[i].GetComponent<PlaceableObjects>();
            EditableObject e = prefabsToSaveList[i].GetComponent<EditableObject>();
            e.LoadMaterials();
            p.placed = ES3.Load<bool>("isAlreadyBougth", SaveAndLoadManager.FileName);
            p.isAlreadyBougth = ES3.Load<bool>("isAlreadyPlaced", SaveAndLoadManager.FileName);
        }
    }

    public void LoadGame()
    {
        Debug.Log("Intento Cargar");
        StartCoroutine(WaitAFramToDestroyObjAndLoad());
    }

    private IEnumerator WaitAFramToDestroyObjAndLoad()
    {
        if (ES3.FileExists(FileName))
        {
            GameObject[] allGo = GameObject.FindGameObjectsWithTag("Furniture");
            if (allGo != null)
            {
                foreach (var variGameObject in allGo)
                {
                    Destroy(variGameObject);
                }
            }
            DesactiveAllConfigBeforeActive();
            yield return new WaitForEndOfFrame();
            Debug.Log("Estoy cargando");
            prefabsToSaveList = ES3.Load("furnituresInstance", FileName, new List<GameObject>());
            prefabConfigSave = ES3.Load<GameObject>("Config", FileName);
            prefabConfigSave.SetActive(true);
            LoadPlaceableBooleans();
            shopAvailable.LoadAmountOfMoney();
            shopSystem.UpdateUI();
            pObjController.FindForActualHouseReferences();
        }
        else
        {
            yield return null;
        }
    }

    private void DesactiveAllConfigBeforeActive()
    {
        allConfigInScene = GameObject.FindGameObjectsWithTag("Config");
        foreach (var config in allConfigInScene)
        {
            config.SetActive(false);
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
