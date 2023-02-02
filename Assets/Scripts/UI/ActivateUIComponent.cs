using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUIComponent : MonoBehaviour
{
    [SerializeField] private GameObject panelToActivate;

    public void ActivateThisUIComponent()
    {
        panelToActivate.SetActive(true);
    }
    
}
