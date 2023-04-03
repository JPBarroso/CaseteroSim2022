using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{

    [SerializeField] private GameObject continuePanel;
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
            continuePanel.SetActive(true);
            button.enabled = false;
        }
        else
        {
            continuePanel.SetActive(false);
        }
    }
}
