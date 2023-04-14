using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomer : MonoBehaviour
{
    Slider _slider;
    [SerializeField] private Camera overlayCam;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = this.GetComponent<Slider>();
    }

    public void OnValueChange()
    {
        Camera.main.orthographicSize = 3.81f + _slider.value;
        overlayCam.orthographicSize = 3.81f + _slider.value;
    }
    public void Reset()
    {
        _slider.value = 10;
    }

}
