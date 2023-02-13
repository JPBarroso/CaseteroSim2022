
using System;
using UnityEngine;

//Clase que se encarga de dar comportamientos a los botones de la UI
public class BuildButtonController : MonoBehaviour
{
    [Header("Shop Data Reference")]
    public Shop shopData;
    
    [Header("Furniture Data Reference")]
    [SerializeField] private Furniture furnitureGlobal;

    [Header("Save Reference")] 
    public SaveAndLoadManager saveManager;
    
    [Header("Controller Reference")] 
    private GameModeController gm;

    private void Start()
    {
        gm = GetComponent<GameModeController>();
        gm.actualMode = GameModeController.GameActualMode.BUILD;
    }

    public void BuildPreviewObjectButton(Furniture furniture)//Construimos el preview del objeto
    {
        if (gm.actualMode == GameModeController.GameActualMode.BUILD)
        {
            furnitureGlobal = furniture;
            BuildingSystem.Instance.PreviewSelectedObj(furnitureGlobal);
            PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
            saveManager.AddGameObjToList(objPLaced.gameObject);
        }
    }

    public void RotatePreviewObjButton(float value)//Rotamos este objeto
    {
        BuildingSystem.Instance.RotateSelectedObj(value);
    }

    public void CancelSelectedObjButton()//Cancelamos compra
    {
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        if (objPLaced.isAlreadyBougth == false)
        {
            saveManager.DeleteFromList(objPLaced.gameObject);
            BuildingSystem.Instance.CancelSelectedObj();
        }
    }

    public void PlaceSelectedObjButton()//Confirmamos la compra del objeto, gastamos dinero y actualizamos la UI
    {
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        if (objPLaced.isAlreadyBougth == false)
        {
            EditableObject editableComponent = objPLaced.GetComponent<EditableObject>();
            editableComponent.ReturnMaterialsWhenFinishEdit();
            BuildingSystem.Instance.PlaceSelectedObjAndBuy();
            shopData.BuyFurniture(furnitureGlobal);
            
            ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
            shopSystem.UpdateUI();
        }

    }
    
    public void StartEditObj()//Este metodo va en el boton de MOVER(Es para empezar a editar). Si el objeto ya está comprado volvemos a meterle el componente drag para mover y rotar
    {
        gm.actualMode = GameModeController.GameActualMode.EDIT;
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        objPLaced.furnitureMode = PlaceableObjects.MODE.Editmode;
        EditableObject editableComponent = objPLaced.GetComponent<EditableObject>();
        editableComponent.ChangeMaterialWhenEdit();
        if (objPLaced.isAlreadyBougth == true)
        {
            if (objPLaced.gameObject.GetComponent<ObjectDrag>() == null)
            {
                objPLaced.gameObject.AddComponent<ObjectDrag>();
            }
            objPLaced.placed = false;
        }
    }

    public void ConfirmEdit(GameObject panel)//Cuando pulsamos en confirmar la edicion volvemos a quitar el componente drag(Igual mas alante activo y desactivo en vez de destruir y añadir)
    {
        panel.SetActive(false);
        if (BuildingSystem.Instance.objToPlace != null)
        {
            PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
            if (objPLaced.isAlreadyBougth)
            {
                objPLaced.furnitureMode = PlaceableObjects.MODE.Putmode;
                EditableObject editableComponent = objPLaced.GetComponent<EditableObject>();
                editableComponent.ReturnMaterialsWhenFinishEdit();
                ObjectDrag drag = objPLaced.GetComponent<ObjectDrag>();
                Destroy(drag);
                objPLaced = null;
                gm.actualMode = GameModeController.GameActualMode.BUILD;
            }
        }

    }
    
    public void SoldItemAfterBuy()//Aqui quiero ver como hcaer para vender los items
    {
        gm.actualMode = GameModeController.GameActualMode.BUILD;
        
        if (BuildingSystem.Instance.objToPlace != null)
        {
            PlaceableObjects objPlaced = BuildingSystem.Instance.objToPlace;
            FurnitureData data = objPlaced.GetComponent<FurnitureData>();
            shopData.ReturnMoney(data.Data);
            ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
            shopSystem.UpdateUI();
            Destroy(objPlaced.gameObject);
        }

    }
    
}
