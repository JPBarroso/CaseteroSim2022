using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Material mat;
    [SerializeField] private Image effect;
    [SerializeField] private ShopSystem sistema;
    [SerializeField] private TMP_InputField casetaNameInputTxt;
    [SerializeField] private TMP_Text casetaNameTxt;
    [SerializeField] private TMP_Text artTxt;
    [SerializeField] private TMP_Text luxTxt;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private string arteNegativo;
    [SerializeField] private string arteNeutral;
    [SerializeField] private string artePositivo;
    [SerializeField] private string lujoNegativo;
    [SerializeField] private string lujoNeutral;
    [SerializeField] private string lujoPositivo;
    [SerializeField] private string totalDinero;
    [SerializeField] private string puntos;


    [Header("Objs and datas")]
    [SerializeField] private GameObject[] allCanvasObj;
    [SerializeField] private FurnitureData[] datas;

    private int artValue;
    private int luxValue;
    private int moneyValue;

    private void OnEnable()
    {
        AllCanvasActivateOrDesactivate(false);
        SetCasetaName();
        SetArtAndLuxValues();
        effect.color = mat.color;
    }

    private void OnDisable()
    {
        AllCanvasActivateOrDesactivate(true);
    }

    //Desactivamos o activamos todos los canvas segun necesitemos
    private void AllCanvasActivateOrDesactivate(bool activated)
    {
        foreach (var canvas in allCanvasObj)
        {
            canvas.SetActive(activated);
        }
    }

    //Cuando activamos este objeto buscamos todos los objetos para setear sus valores de arte y lujo y mostrarlos en pantalla
    private void SetArtAndLuxValues()
    {
        datas = FindObjectsOfType<FurnitureData>();

        for (int i = 0; i < datas.Length; i++)
        {
            artValue += datas[i].Data.ArtisticValue;
            luxValue += datas[i].Data.LuxuryValue;
        }
        switch (artValue)
        {
            case < 0:
                artTxt.text = arteNegativo + " " + MathF.Abs(artValue) + " " + puntos;
                break;
            case 0:
                artTxt.text = arteNeutral;
                break;
            case > 0:
                artTxt.text = artePositivo + " " + artValue + " " + puntos;
                break;
        }
        switch (luxValue)
        {
            case < 0:
                luxTxt.text = lujoNegativo + " " + MathF.Abs(luxValue) + " " + puntos;
                break;
            case 0:
                luxTxt.text = lujoNeutral;
                break;
            case > 0:
                luxTxt.text = lujoPositivo + " " + Mathf.Abs(luxValue) + " " + puntos;
                break;
        }
        moneyValue = sistema.EndMoney();
        moneyText.text = totalDinero + " " + moneyValue.ToString() + ".";
        
        artTxt.text = "El valor de arte es" + artValue.ToString();
        luxTxt.text = "El valor de lujo es" + luxValue.ToString();
    }

    private void SetCasetaName()
    {
        casetaNameTxt.text = ES3.FileExists(SaveAndLoadManager.FileName) ? CasetaNameLoad() : casetaNameInputTxt.text;
    }
    
    private string CasetaNameLoad()
    {
        return ES3.Load<string>("CasetaName", SaveAndLoadManager.FileName);
    }


}
