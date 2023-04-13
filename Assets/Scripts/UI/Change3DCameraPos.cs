using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change3DCameraPos : MonoBehaviour
{

    [SerializeField] private Transform[] camera3DTransform;
    [SerializeField] private Camera camera3D;

    private int transformIndex;

    private void Start()
    {
        transformIndex = 0;
        camera3D.transform.position = camera3DTransform[transformIndex].position;
        camera3D.transform.rotation = camera3DTransform[transformIndex].rotation;
    }

    private void Update()
    {
        Debug.Log(camera3DTransform.Length);
        Debug.Log(transformIndex);
    }


    //Cambio el transform de la camara a otro punto
    public void ChangeCamera3DTransform()
    {
        if (transformIndex < camera3DTransform.Length - 1)
        {
            transformIndex += 1;
            camera3D.transform.position = camera3DTransform[transformIndex].position;
            camera3D.transform.rotation = camera3DTransform[transformIndex].rotation;
        }
        else
        {
            transformIndex = 0;
            camera3D.transform.position = camera3DTransform[transformIndex].position;
            camera3D.transform.rotation = camera3DTransform[transformIndex].rotation;
        }

    }
    

}
