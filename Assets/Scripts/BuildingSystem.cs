using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    //Instancia singleton
    public static BuildingSystem Instance;

    [Header("Grid stuffs")]
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTileMap;
    [SerializeField] private TileBase whiteTile;

    [Header("GameObj variables")]
    public bool isPlacingAObj;
    public PlaceableObjects objToPlace;
    
    //Inicialzacion
    private void Awake()
    {
        Instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    //BUILD METHODS//
    public void PreviewSelectedObj(Furniture furniture)
    {
        //Si no tenemos un objeto puesto, instanciamos el prefab tirando desde el scriptable Furniture que pasamos por param
        if (!isPlacingAObj)
        {
            InitializeWithObj(furniture);
            isPlacingAObj = true;
        }
        else if (!isPlacingAObj)
        {
            InitializeWithObj(furniture);
            isPlacingAObj = true;
        }
    }

    //Llamamos al método Rotate para rotar el objeto puesto(objToPlace)
    public void RotateSelectedObj(float value)
    {
        if (!objToPlace.placed)
        {
            objToPlace.Rotate(value);
        }
    }
    
    //Este método es para varias su posicion en Y. Por ahora está descartado pero lo dejo por si algo
    public void ChangePositionOfSelectedObj(float value)
    {
        if (!objToPlace.placed)
        {
            objToPlace.ChangeYPositionOfObject(objToPlace.gameObject, value);
        }
    }

    //Colocamos el objeto y aplicamos el Place(esto lo coloca y lo compra además de poner el Place a true)
    public void PlaceSelectedObjAndBuy()
    {
        if (CanBePlaced(objToPlace))
        {
            objToPlace.furnitureMode = PlaceableObjects.MODE.Buymode;//Cambiamos el modo
            objToPlace.Place();//Colocamos el prefab
            Vector3Int start = gridLayout.WorldToCell(objToPlace.GetStartPosition());//Posicionamos en el grid
            TakeArea(start, objToPlace.Size);//Esto es para colocar los cuadritos blancos, es temporal.
            isPlacingAObj = false;
        }
        else
        {
            //Destroy(objToPlace.gameObject);
            Debug.Log("No puedes hacer place ahi");
        }
    }

    //Si nos hemos arrepentido, este metodo cancela la construcción
    public void CancelSelectedObj()
    {
        if (objToPlace.placed)
        {
            return;
        }

        isPlacingAObj = false;
        Destroy(objToPlace.gameObject);
    }

    
    ///////////////Pôsiciones//////////////////////////////////////
    //Retornamos la posicion del raton en caso de que toque algo
    public Vector3 GetMouseWorldPosition()
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
    private void InitializeWithObj(Furniture furniture)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(furniture.furniturePrefab, position, Quaternion.identity);//Instan el obj
        objToPlace = obj.GetComponent<PlaceableObjects>();
        obj.AddComponent<ObjectDrag>();//Añadimos el componente drag para poder moverlo con el raton
        obj.layer = LayerMask.NameToLayer("Build");
        Transform[] allObj = obj.GetComponentsInChildren<Transform>();
        foreach (var g in allObj)
        {
            g.gameObject.layer = LayerMask.NameToLayer("Build");
        }
        tempObjDrag = objToPlace.GetComponent<ObjectDrag>();

    }

    private bool CanBePlaced(PlaceableObjects placeableObjects)//Esto debería de detectar cosas de detectar para ver si podemos colocar o no pero no me funciona bien del todo
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

    private void TakeArea(Vector3Int start, Vector3Int size)//Para los cuadritos blancos
    {
        //mainTileMap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
    }
    
}
