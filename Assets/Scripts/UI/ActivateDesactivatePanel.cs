using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDesactivatePanel : MonoBehaviour
{
    
    [SerializeField] private GameObject panel;
    
    private void OnEnable()
    {
        panel.SetActive(false);
    }

    private void OnDisable()
    {
        //panel.SetActive(true);
    }
}
