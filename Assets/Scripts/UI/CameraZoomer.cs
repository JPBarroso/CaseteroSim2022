using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomer : MonoBehaviour
{
    Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = this.GetComponent<Slider>();
    }

    public void OnValueChange()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, _slider.value, Camera.main.transform.position.z);
    }
    public void Reset()
    {
        _slider.value = 10;
    }

}
