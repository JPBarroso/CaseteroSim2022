using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EditorUpdater : MonoBehaviour
{
    [ExecuteInEditMode]

    public Furniture datos;
    public TextMeshProUGUI nombreTMP;
    public TextMeshProUGUI precioTMP;
    public Image muebleSprite;
    Furniture.FurnitureColor colormueble;
    public Image Base;

    Color32 rojobase = new Color32(226, 79, 71, 255);
    Color32 rojoHL = new Color32(144, 47, 47, 167);
    Color32 marronbase = new Color32(164,145,107,255);
    Color32 marronHL = new Color32(154,121,85,167);

    [Header("Colores")]
    public Color32 blancobase, blancoHL, negrobase, negroHL, verdebase, verdeHL;

    public GameObject lujo, eco, arte, simple;

    // Update is called once per frame
    [ContextMenu("Pablo puto")]
    void Update()
    {
        this.transform.name = datos.name;
        colormueble = datos.colorEnum;
        nombreTMP.text = datos.furnitureName;
        precioTMP.text = datos.furniturePrice.ToString();
        muebleSprite.sprite = datos.spriteFurniture;
        //EffectColourer();
        StatsShow();
        this.enabled = false;
    }

    //void EffectColourer()
    //{
    //    switch (colormueble)
    //    {
    //        case Furniture.FurnitureColor.RED:
    //            Base.color = rojobase;
    //            Effect.color = rojoHL;
    //            break;
    //        case Furniture.FurnitureColor.BROWN:
    //            Base.color = marronbase;
    //            Effect.color = marronHL;
    //            break;
    //        case Furniture.FurnitureColor.GREEN:
    //            Base.color = verdebase;
    //            Effect.color = verdeHL;
    //            break;
    //        case Furniture.FurnitureColor.WHITE:
    //            Base.color = blancobase;
    //            Effect.color = blancoHL;
    //            break;
    //        case Furniture.FurnitureColor.BLACK:
    //            Base.color = negrobase;
    //            Effect.color = negroHL;
    //            break;
    //    }
    //}
    void StatsShow()
    {
        lujo.SetActive(false);
        eco.SetActive(false);
        arte.SetActive(false);
        simple.SetActive(false);
        if(datos.ArtisticValue > 0)
        {
            arte.SetActive(true);
        }
        else if(datos.ArtisticValue < 0)
        {
            simple.SetActive(true);
        }
        if(datos.LuxuryValue > 0)
        {
            lujo.SetActive(true);
        }
        else if(datos.LuxuryValue < 0)
        {
            eco.SetActive(true);
        }
    }
}
