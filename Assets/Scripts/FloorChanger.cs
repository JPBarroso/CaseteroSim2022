using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChanger : MonoBehaviour
{
    public MeshRenderer suelo;
    public PermanentsObjController objController;

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
    }

    public void SaveMaterial()
    {
        ES3.Save("SueloMaterial", suelo.material, SaveAndLoadManager.FileName);
    }
}
