using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeBase : MonoBehaviour
{
    public bool esDesafio;
    [SerializeField] private FurnitureData[] datas;
    [SerializeField] private int artValue, luxValue,bars,chairs,tables,electronics;
    [SerializeField] private int rojo, verde, blanco, negro, marron;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Calculate()
    {
        Debug.Log("Se calcula");
        artValue = 0;
        luxValue = 0;
        bars = 0;
        chairs = 0;
        tables = 0;
        electronics = 0;
        rojo = 0;
        verde = 0;
        blanco = 0;
        negro = 0;
        marron = 0;

        datas = FindObjectsOfType<FurnitureData>();

        for (int i = 0; i < datas.Length; i++)
        {
            artValue += datas[i].Data.ArtisticValue;
            luxValue += datas[i].Data.LuxuryValue;
            switch (datas[i].Data.typeEnum)
            {
                case Furniture.FurnitureType.CHAIR:
                    chairs++;
                    break;
                case Furniture.FurnitureType.TABLE:
                    tables++;
                    break;
                case Furniture.FurnitureType.BAR:
                    bars++;
                    break;
                case Furniture.FurnitureType.ELECTRONICS:
                    electronics++;
                    break;
            }
            switch (datas[i].Data.colorEnum)
            {
                case Furniture.FurnitureColor.RED:
                    rojo++;
                    break;
                case Furniture.FurnitureColor.GREEN:
                    verde++;
                    break;
                case Furniture.FurnitureColor.WHITE:
                    blanco++;
                    break;
                case Furniture.FurnitureColor.BLACK:
                    negro++;
                    break;
                case Furniture.FurnitureColor.BROWN:
                    marron++;
                    break;
            }
        }
    }
}
