using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Clase para actualiar la UI
public class ShopSystem : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        shop.InitAmountOfMoney();
        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyText.text = shop.moneyAvailable.ToString(CultureInfo.CurrentCulture);
    }
}
