using System;
using TMPro;
using UnityEngine;

public class FinishPanel : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TMP_InputField casetaNameInputTxt;
    [SerializeField] private TMP_Text casetaNameTxt;
    [SerializeField] private TMP_Text artTxt;
    [SerializeField] private TMP_Text luxTxt;
    
    [Header("Objs and datas")]
    [SerializeField] private GameObject[] allCanvasObj;
    [SerializeField] private FurnitureData[] datas;

    private int artValue;
    private int luxValue;

    private void OnEnable()
    {
        AllCanvasActivateOrDesactivate(false);
        SetCasetaName();
        SetArtAndLuxValues();
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
