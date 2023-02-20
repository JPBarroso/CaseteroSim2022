using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Shops Data")]
public class Shop : ScriptableObject
{
    public float maxMoney;
    public float moneyAvailable;

    public void InitAmountOfMoney()
    {
        moneyAvailable = maxMoney;
    }

    public void LoadAmountOfMoney()
    {
        if (!ES3.FileExists(SaveAndLoadManager.FileName))
        {
            moneyAvailable = maxMoney;
        }
        else
        {
            moneyAvailable = ES3.Load<float>("Money", SaveAndLoadManager.FileName);
        }
    }

    public void BuyFurniture(Furniture furniture)
    {
        if (moneyAvailable >= furniture.furniturePrice)
        {
            moneyAvailable -= furniture.furniturePrice;
        }
    }

    public void ReturnMoney(Furniture furniture)
    {
        moneyAvailable += furniture.furniturePrice;
    }

    public void BuyAConfiguration(HouseConfig houseConfig)
    {
        moneyAvailable -= houseConfig.price;
    }

    public void ReturnBuyConfigurationMoney(HouseConfig houseConfig)
    {
        moneyAvailable += houseConfig.price;
    }
}
