using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{

    public UnityEvent eventMaterial;

    public void InvokeEventIfMoney()
    {
        Debug.Log("lanzando el evento segun el boton");
        eventMaterial.Invoke();
    }
    
    
}
