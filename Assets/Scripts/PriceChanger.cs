using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PriceChanger : MonoBehaviour
{
    [SerializeField]
    HouseConfig _config;
    TextMeshProUGUI _textoprecio;
    int _precioInicial;
    [SerializeField]
    PermanentsObjController _POC;
    // Es San Valentín y lo estoy pasando con Visual Studio
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PriceChange");
        _precioInicial = (int)_config.price;
        _textoprecio = this.GetComponent<TextMeshProUGUI>();
        PriceChange();
    }

    void PriceChange()
    {
        float nuevoPrecio = (_precioInicial - _POC.priceActualConfigBuy); //He tenido que hacer público un valor de Jose y me va a matar
        if(nuevoPrecio > 0)
        {
            _textoprecio.color = Color.red;
            _textoprecio.text = "- " + Mathf.Abs(nuevoPrecio).ToString();
        }
        else if(nuevoPrecio < 0)
        {
            _textoprecio.color = Color.green;
            _textoprecio.text = "+ " + Mathf.Abs(nuevoPrecio).ToString();
        }
        else
        {
            _textoprecio.color = Color.black;
            _textoprecio.text = nuevoPrecio.ToString();
        }
    }

}
