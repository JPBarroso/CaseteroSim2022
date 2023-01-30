using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{

    public static BuildingSystem Instance;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTileMap;
    [SerializeField] private TileBase whiteTile;
    [SerializeField] private TileBase redTile;

    public GameObject prefab1;
    public GameObject prefab2;
    public bool isPlacingAObj;

    private PlaceableObjects objToPlace;


    //Inicialzacion
    private void Awake()
    {
        Instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        //Creamos un objeto que seguirá al ratón
        if (!objToPlace)
        {
            return;
        }
    }
    
    //BUILD METHODS//
    public void PreviewSelectedObj(GameObject prefabSelected)
    {
        if (!isPlacingAObj)
        {
            InitializeWithObj(prefabSelected);
            isPlacingAObj = true;
        }
        else if (!isPlacingAObj)
        {
            InitializeWithObj(prefabSelected);
            isPlacingAObj = true;
        }
    }

    public void RotateSelectedObj()
    {
        if (!objToPlace.Placed)
        {
            objToPlace.Rotate();
        }
    }

    public void PlaceSelectedObjAndBuy()
    {
        if (CanBePlaced(objToPlace) && tempObjDrag.canBePlaced)
        {
            objToPlace.Place();
            Vector3Int start = gridLayout.WorldToCell(objToPlace.GetStartPosition());
            TakeArea(start, objToPlace.Size);
            isPlacingAObj = false;
        }
        else
        {
            Destroy(objToPlace.gameObject);
            Debug.Log("No puedes hacer place ahi");
        }
    }

    public void CancelSelectedObj()
    {
        if (objToPlace.Placed)
        {
            return;
        }

        isPlacingAObj = false;
        Destroy(objToPlace.gameObject);
    }

    
    ///////////////Pôsiciones//////////////////////////////////////
    //Retornamos la posicion del raton en caso de que toque algo
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out  RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    //Retornamos la posicion dentro del grid
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] tilebaseArray = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var vector in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(vector.x, vector.y, 0);
            tilebaseArray[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return tilebaseArray;
    }
    
    /////////////////////////////////////////////////
    //Prefabs instanciacion
    private ObjectDrag tempObjDrag;
    public void InitializeWithObj(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objToPlace = obj.GetComponent<PlaceableObjects>();
        obj.AddComponent<ObjectDrag>();

        tempObjDrag = objToPlace.GetComponent<ObjectDrag>();

    }

    private bool CanBePlaced(PlaceableObjects placeableObjects)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objToPlace.GetStartPosition());
        area.size = placeableObjects.Size;

        TileBase[] baseArray = GetTilesBlock(area, mainTileMap);

        foreach (var tileBase in baseArray)
        {
            if (tileBase == whiteTile)
            {
                Debug.Log("Detectando tile blanco");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        mainTileMap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
    }
    
}
