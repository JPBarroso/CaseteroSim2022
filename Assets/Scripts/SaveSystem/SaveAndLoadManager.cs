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
    [SerializeField] private FloorChanger floorChanger;
    [SerializeField] private InputTextChanger inputFileTxt;

    private GameObject[] allConfigInScene;
    private int sceneIndex;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void Start()
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
        Debug.Log("Borrando");
        ES3.DeleteFile(FileName);
        Debug.Log("empieza");
        SavePrefabs();
        SaveMoney();
        SaveConfig();
        inputFileTxt.SetCasetaString();
        floorChanger.SaveMaterial();
        SaveSceneIndex();
        Debug.Log("acaba");
    }

    private void SaveSceneIndex()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        ES3.Save("sceneIndex",sceneIndex, FileName);
    }

    private void SavePrefabs()
    {
        if (prefabsToSaveList.Count > 0)
        {
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
        ES3.Save("CasetaConfig", prefabConfigSave, FileName);
    }

    private void LoadPlaceableBooleans()
    {
        for (int i = 0; i < prefabsToSaveList.Count; i++)
        {
            PlaceableObjects p = prefabsToSaveList[i].GetComponent<PlaceableObjects>();
            EditableObject e = prefabsToSaveList[i].GetComponent<EditableObject>();
            p.placed = ES3.Load<bool>("isAlreadyBougth", SaveAndLoadManager.FileName);
            p.isAlreadyBougth = ES3.Load<bool>("isAlreadyPlaced", SaveAndLoadManager.FileName);
            Debug.Log("Cargo cosas y esta comprado?" + p.isAlreadyBougth);
            e.LoadMaterials();
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
            prefabsToSaveList = ES3.Load("furnituresInstance", FileName, new List<GameObject>());
            prefabConfigSave = ES3.Load<GameObject>("CasetaConfig", FileName);
            prefabConfigSave.SetActive(true);
            floorChanger.LoadFloorMaterial();
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
        HouseConfigData[] houseConfigDatas = FindObjectsOfType<HouseConfigData>();
        
        foreach (var config in houseConfigDatas)
        {
            config.gameObject.SetActive(false);
        }

    }

    public void DeleteGame()
    {
        Debug.Log("Borrando");
        ES3.DeleteFile(FileName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    
}
