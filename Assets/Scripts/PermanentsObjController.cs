using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Clase que controlla la construccion de los items fijos. Estan las cosas con serialized para hacer comprobaciones en el editor
public class PermanentsObjController : MonoBehaviour
{
    [Header("Shop Reference")]
    [SerializeField] private Shop shopData;
    
    [Header("Actual Obj References")]
    [SerializeField] private HouseConfigData actualDataInScene;
    [SerializeField] private GameObject actualObjHouseInScene;
    [SerializeField] private HouseConfig actualHouseConfig;
    public float priceActualConfigBuy;
    
    [Header("Latest Obj References")]
    [SerializeField] private HouseConfigData latestDataInScene;
    [SerializeField] private GameObject latestHouseObj;
    [SerializeField] private HouseConfig latestHouseConfig;
    [SerializeField] private float priceLatestConfigBuy;

    [SerializeField] private SaveAndLoadManager saveManager;

    AudioManager mgr;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        mgr = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        actualDataInScene = FindObjectOfType<HouseConfigData>();
        actualObjHouseInScene = actualDataInScene.gameObject;
        actualHouseConfig = actualDataInScene.config;
        priceActualConfigBuy = actualHouseConfig.price;
        
        saveManager.AddToPlaceReference(actualObjHouseInScene);
    }

    public void BuildANewConfig(GameObject newBuy)
    {
        HouseConfig houseConfigData = newBuy.GetComponent<HouseConfigData>().config;
        EventTest eventTest = EventSystem.current.currentSelectedGameObject.GetComponent<EventTest>();

        if (newBuy.activeInHierarchy || houseConfigData.price > shopData.moneyAvailable) return;//Si clicamos en un objeto ya existentes nada

        LatestHouseReference();//Guardamos los datos en la "ultima compra"
        actualObjHouseInScene.SetActive(false);//Desactivamos este objeto
        newBuy.SetActive(true);//Activamos la nueva configuracion de caseta
        eventTest.InvokeEventIfMoney();
        //Aplicamos referencias sobre el nuevo objeto
        actualObjHouseInScene = newBuy;
        NewHouseReferences(newBuy);
        saveManager.AddToPlaceReference(newBuy);
        //Descontamos el dinero
        CheckForPriceDifference();
        UpdateUI();
    }

    private void LatestHouseReference()
    {
        latestDataInScene = actualDataInScene;
        latestHouseObj = actualObjHouseInScene;
        latestHouseConfig = actualHouseConfig;
        priceLatestConfigBuy = actualHouseConfig.price;
    }
    
    private void NewHouseReferences(GameObject newBuy)
    {
        actualDataInScene = newBuy.GetComponent<HouseConfigData>();
        actualHouseConfig = actualDataInScene.config;
        priceActualConfigBuy = actualHouseConfig.price;
    }

    public void FindForActualHouseReferences()
    {
        actualDataInScene = FindObjectOfType<HouseConfigData>();
        actualHouseConfig = actualDataInScene.config;
        actualObjHouseInScene = actualDataInScene.gameObject;
        priceActualConfigBuy = actualHouseConfig.price;
    }

    private void CheckForPriceDifference()
    {
        shopData.ReturnBuyConfigurationMoney(latestHouseConfig);
        shopData.BuyAConfiguration(actualHouseConfig);
    }

    private void UpdateUI()
    {
        mgr.CompraSFX();
        ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
        shopSystem.UpdateUI();
        NotificationCenter.DefaultCenter().PostNotification(this,"PriceChange");
    }
    
    
}
