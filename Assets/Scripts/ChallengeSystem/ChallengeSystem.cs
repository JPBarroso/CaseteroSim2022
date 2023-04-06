using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeSystem : MonoBehaviour
{
    [SerializeField] private Challenge[] desafios;
    [SerializeField] private FurnitureData[] datas;
    [SerializeField] private ShopSystem sistema;

    private int artValue;
    private int luxValue;
    private int moneyValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Calculate()
    {
        datas = FindObjectsOfType<FurnitureData>();
        for (int i = 0; i < datas.Length; i++)
        {
            artValue += datas[i].Data.ArtisticValue;
            luxValue += datas[i].Data.LuxuryValue;
            moneyValue += (int)datas[i].Data.furniturePrice;
        }

    }
    void ReadChallenge()
    {

    }
    
    

}
