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

    public void BuyFurniture(Furniture furniture)
    {
        if (moneyAvailable >= furniture.furniturePrice)
        {
            moneyAvailable -= furniture.furniturePrice;
        }
        
    }
}
