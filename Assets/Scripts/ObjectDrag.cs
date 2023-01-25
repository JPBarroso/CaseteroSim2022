using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{

    //Estos métodos habrá que investigar como usarlos con el touch del móvil
    private Vector3 offset;

    private void Update()
    {
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = BuildingSystem.Instance.SnapCoordinateToGrid(pos);
    }

    private void OnMouseDown()
    {
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        /*
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = BuildingSystem.Instance.SnapCoordinateToGrid(pos);
        */
    }
}
