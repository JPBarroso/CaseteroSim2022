using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DesactivateButtonIfDataNoExist();
    }

    private void DesactivateButtonIfDataNoExist()
    {
        if (!ES3.FileExists(SaveAndLoadManager.FileName))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
