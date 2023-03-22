using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfigEditorUpdater : MonoBehaviour
{
    [ExecuteInEditMode]
    public HouseConfig config;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI precio;


    // Update is called once per frame
    [ContextMenu("Nomura cabrón")]
    void Update()
    {
        this.name = config.configName;
        nombre.text = config.configName;
        precio.text = config.price.ToString();
        this.enabled = false;
    }
}
