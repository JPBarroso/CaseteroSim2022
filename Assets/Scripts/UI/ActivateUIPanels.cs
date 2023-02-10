using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUIPanels : MonoBehaviour
{

    public void ActivateThisPanel(GameObject panelToActivate)
    {
        Debug.Log("test");
        panelToActivate.gameObject.SetActive(!panelToActivate.activeInHierarchy);
    }
    
    
}
