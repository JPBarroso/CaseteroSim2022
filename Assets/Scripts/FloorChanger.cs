using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChanger : MonoBehaviour
{
    public MeshRenderer suelo;
    
    public void CambiaSuelo(Material nuevomat)
    {
        suelo.material = nuevomat;
    }
}
