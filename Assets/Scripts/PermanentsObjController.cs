using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentsObjController : MonoBehaviour
{
    [Header("Shop Reference")]
    [SerializeField] private Shop shopData;
    [Header("Actual Obj References")]
    [SerializeField] private HouseConfigData actualDataInScene;
    [SerializeField] private GameObject actualObjHouseInScene;
    [SerializeField] private HouseConfig actualHouseConfig;
    [Header("Latest Obj References")]
    [SerializeField] private HouseConfigData latestDataInScene;
    [SerializeField] private GameObject latestHouseObj;
    [SerializeField] private HouseConfig latestHouseConfig;


    private void Awake()
    {
        actualDataInScene = FindObjectOfType<HouseConfigData>();
        actualObjHouseInScene = actualDataInScene.gameObject;
        actualHouseConfig = actualDataInScene.config;
    }

    public void BuildANewConfig(GameObject newBuy)
    {
        if (newBuy.activeInHierarchy) return;//Si clicamos en un objeto ya existentes nada

        LatestHouseReference();//Guardamos los datos en la "ultima compra"
        actualObjHouseInScene.SetActive(false);//Desactivamos este objeto
        newBuy.SetActive(true);//Activamos la nueva configuracion de caseta
        
        //Aplicamos referencias sobre el nuevo objeto
        actualObjHouseInScene = newBuy;
        
        UpdateUI();
        NewHouseReferences(newBuy);
        
        shopData.BuyAConfiguration(actualHouseConfig);
    }

    private void LatestHouseReference()
    {
        latestDataInScene = actualDataInScene;
        latestHouseObj = actualObjHouseInScene;
        latestHouseConfig = actualHouseConfig;
    }
    
    private void NewHouseReferences(GameObject newBuy)
    {
        actualDataInScene = newBuy.GetComponent<HouseConfigData>();
        actualHouseConfig = actualDataInScene.config;
    }

    private void UpdateUI()
    {
        ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
        shopSystem.UpdateUI();
    }
    
    
}
