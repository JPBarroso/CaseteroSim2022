using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUIPanels : MonoBehaviour
{

    public void ActivateThisPanel(GameObject panelToActivate)
    {
        panelToActivate.gameObject.SetActive(!panelToActivate.activeInHierarchy);
    }
    
    
}
