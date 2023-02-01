using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
