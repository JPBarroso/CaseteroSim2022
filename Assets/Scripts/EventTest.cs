using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{

    public UnityEvent eventMaterial;

    public void InvokeEventIfMoney()
    {
        eventMaterial.Invoke();
    }
    
    
}
