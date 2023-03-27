using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChanger : MonoBehaviour
{
    public MeshRenderer suelo;

    private void Start()
    {
        if (ES3.FileExists(SaveAndLoadManager.FileName))
        {
            suelo.material = ES3.Load<Material>("SueloMaterial", SaveAndLoadManager.FileName);
        }
    }

    public void CambiaSuelo(Material nuevomat)
    {
        suelo.material = nuevomat;
        ES3.Save("SueloMaterial", nuevomat, SaveAndLoadManager.FileName);
    }
}
