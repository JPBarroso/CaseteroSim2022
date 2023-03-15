using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    Material material;
    public Color _rojo;
    public Color _verde;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void cambiaColor()
    {
        if(this.GetComponent<Slider>().value == 0)
        {
            material.color = _rojo;
        }
        else
        {
            material.color = _verde;
        }
    }
    public void cambiaBoton(int valor)
    {
        if(valor == 0)
        {
            material.color = _rojo;
        }
        else
        {
            material.color = _verde;
        }
    }
}
