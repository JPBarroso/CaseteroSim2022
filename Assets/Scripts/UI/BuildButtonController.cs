
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

    [Header("Canvas Reference")] 
    [SerializeField] private GameObject buyObjPanel;
    [SerializeField] private GameObject editPanel;
    
    
    AudioManager mgr;


    private void Start()
    {
        mgr = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gm = GetComponent<GameModeController>();
        //gm.actualMode = GameModeController.GameActualMode.BUILD;
    }

    public void BuildPreviewObjectButton(Furniture furniture)//Construimos el preview del objeto
    {
        if (furniture.furniturePrice <= shopData.moneyAvailable)
        {
            if (GameModeController.Instance.actualMode == GameModeController.GameActualMode.WAIT)
            {
                GameModeController.Instance.actualMode = GameModeController.GameActualMode.BUILD;
            }
        
            if (gm.actualMode == GameModeController.GameActualMode.BUILD)
            {
                buyObjPanel.SetActive(true);
                mgr.ButtonSFX();
                furnitureGlobal = furniture;
                if (furnitureGlobal.furniturePrice <= shopData.moneyAvailable)
                {
                    BuildingSystem.Instance.PreviewSelectedObj(furnitureGlobal);
                    PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
                    saveManager.AddGameObjToList(objPLaced.gameObject);
                }
            }
        }

    }

    public void RotatePreviewObjButton(float value)//Rotamos este objeto
    {
        mgr.RotacionSFX();
        BuildingSystem.Instance.RotateSelectedObj(value);
    }

    public void CancelSelectedObjButton()//Cancelamos compra
    {
        mgr.ButtonSFX();
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        if (objPLaced.isAlreadyBougth == false)
        {
            saveManager.DeleteFromList(objPLaced.gameObject);
            BuildingSystem.Instance.CancelSelectedObj();
            GameModeController.Instance.actualMode = GameModeController.GameActualMode.WAIT;
        }
    }

    public void PlaceSelectedObjButton()//Confirmamos la compra del objeto, gastamos dinero y actualizamos la UI
    {
        mgr.CompraSFX();
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        ObjectDrag objDrag = objPLaced.GetComponent<ObjectDrag>();
        if (objPLaced.isAlreadyBougth == false && objDrag.canBePlaced)
        {
            EditableObject editableComponent = objPLaced.GetComponent<EditableObject>();
            editableComponent.ReturnMaterialsWhenFinishEdit();
            editableComponent.SaveLastPositionBeforeEdite();
            BuildingSystem.Instance.PlaceSelectedObjAndBuy();
            shopData.BuyFurniture(furnitureGlobal);
            
            ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
            shopSystem.UpdateUI();
            GameModeController.Instance.actualMode = GameModeController.GameActualMode.WAIT;
        }

    }
    
    public void StartEditObj()//Este metodo va en el boton de MOVER(Es para empezar a editar). Si el objeto ya está comprado volvemos a meterle el componente drag para mover y rotar
    {
        mgr.ButtonSFX();
        if (GameModeController.Instance.actualMode == GameModeController.GameActualMode.EDIT)
        {
            gm.actualMode = GameModeController.GameActualMode.EDITING;
            PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
            
            objPLaced.furnitureMode = PlaceableObjects.MODE.Editmode;
            EditableObject editableComponent = objPLaced.GetComponent<EditableObject>();
            editableComponent.ChangeMaterialWhenEdit();
            editableComponent.SaveLastPositionBeforeEdite();
            if (objPLaced.isAlreadyBougth == true)
            {
                if (objPLaced.gameObject.GetComponent<ObjectDrag>() == null)
                {
                    objPLaced.gameObject.AddComponent<ObjectDrag>();
                }
                objPLaced.placed = false;
            }
        }
    }

    public void ConfirmEdit(GameObject panel)//Cuando pulsamos en confirmar la edicion volvemos a quitar el componente drag(Igual mas alante activo y desactivo en vez de destruir y añadir)
    {
        mgr.CompraSFX();
        if (BuildingSystem.Instance.objToPlace != null)
        {
            PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
            ObjectDrag objDrag = objPLaced.GetComponent<ObjectDrag>();
            if (objPLaced.isAlreadyBougth  && objDrag.canBePlaced)
            {
                FindSimsPickAndDestro();
                panel.SetActive(false);
                objPLaced.furnitureMode = PlaceableObjects.MODE.Putmode;
                EditableObject editableComponent = objPLaced.GetComponent<EditableObject>();
                editableComponent.ReturnMaterialsWhenFinishEdit();
                editableComponent.SaveLastPositionBeforeEdite();
                ObjectDrag drag = objPLaced.GetComponent<ObjectDrag>();
                Destroy(drag);
                objPLaced = null;
                gm.actualMode = GameModeController.GameActualMode.WAIT;
                editPanel.SetActive(false);
            }
        }

    }
    
    public void SoldItemAfterBuy()
    {
        mgr.CompraSFX();
        gm.actualMode = GameModeController.GameActualMode.WAIT;
        
        if (BuildingSystem.Instance.objToPlace != null)
        {
            PlaceableObjects objPlaced = BuildingSystem.Instance.objToPlace;
            FurnitureData data = objPlaced.GetComponent<FurnitureData>();
            shopData.ReturnMoney(data.Data);
            saveManager.DeleteFromList(objPlaced.gameObject);
            ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
            shopSystem.UpdateUI();
            Destroy(objPlaced.gameObject);
        }

    }

    public void CancelEdit()
    {
        mgr.ButtonSFX();
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        EditableObject editableComponent = objPLaced.GetComponent<EditableObject>();
        editableComponent.ReturnToLastPositionTest();
        editableComponent.ReturnMaterialsWhenFinishEdit();
    }

    public void FindSimsPickAndDestro()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Pick");
        if (temp != null)
        {
            Destroy(temp);
        }
    }
    
}
