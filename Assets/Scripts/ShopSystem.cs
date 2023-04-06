using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Clase para actualiar la UI
public class ShopSystem : MonoBehaviour
{
    [SerializeField] bool esDesafio = false;
    [SerializeField] private Shop shop;
    [SerializeField] private TMP_Text moneyText;
    public int inicial;

    private void Start()
    {
        shop.InitAmountOfMoney();
        UpdateUI();
        inicial = (int)shop.moneyAvailable;
    }

    public void UpdateUI()
    {
        moneyText.text = shop.moneyAvailable.ToString(CultureInfo.CurrentCulture);
        if (esDesafio)
        {
            GetComponent<ChallengeBase>().Calculate();
        }
    }

    public int EndMoney()
    {
        int final = inicial - (int)shop.moneyAvailable;
        return final;
    }
}
