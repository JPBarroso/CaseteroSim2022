using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private void Awake()
    {
        actualDataInScene = FindObjectOfType<HouseConfigData>();
        actualObjHouseInScene = actualDataInScene.gameObject;
        actualHouseConfig = actualDataInScene.config;
        priceActualConfigBuy = actualHouseConfig.price;
    }

    public void BuildANewConfig(GameObject newBuy)
    {
        if (newBuy.activeInHierarchy) return;//Si clicamos en un objeto ya existentes nada

        LatestHouseReference();//Guardamos los datos en la "ultima compra"
        actualObjHouseInScene.SetActive(false);//Desactivamos este objeto
        newBuy.SetActive(true);//Activamos la nueva configuracion de caseta
        
        //Aplicamos referencias sobre el nuevo objeto
        actualObjHouseInScene = newBuy;
        NewHouseReferences(newBuy);
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

    private void CheckForPriceDifference()
    {
        shopData.ReturnBuyConfigurationMoney(latestHouseConfig);
        shopData.BuyAConfiguration(actualHouseConfig);
    }

    private void UpdateUI()
    {
        ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
        shopSystem.UpdateUI();
        NotificationCenter.DefaultCenter().PostNotification(this,"PriceChange");
    }
    
    
}
