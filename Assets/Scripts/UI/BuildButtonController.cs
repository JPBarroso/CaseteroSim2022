
using UnityEngine;

public class BuildButtonController : MonoBehaviour
{
    public Shop shopTest;
    private Furniture furnitureGlobal;
    
    public void BuildPreviewObjectButton(Furniture furniture)
    {
        furnitureGlobal = furniture;
        BuildingSystem.Instance.PreviewSelectedObj(furnitureGlobal);
    }

    public void RotatePreviewObjButton(float value)
    {
        BuildingSystem.Instance.RotateSelectedObj(value);
    }

    public void CancelSelectedObjButton()
    {
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        if (objPLaced.isAlreadyBougth == false)
        {
            BuildingSystem.Instance.CancelSelectedObj();
        }
    }

    public void PlaceSelectedObjButton()
    {
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        if (objPLaced.isAlreadyBougth == false)
        {
            BuildingSystem.Instance.PlaceSelectedObjAndBuy();
            ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
            shopTest.BuyFurniture(furnitureGlobal);
            shopSystem.UpdateUI();
        }

    }
    
    public void StartEditObj()
    {
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        objPLaced.furnitureMode = PlaceableObjects.MODE.Editmode;
        if (objPLaced.isAlreadyBougth == true)
        {
            if (objPLaced.gameObject.GetComponent<ObjectDrag>() == null)
            {
                objPLaced.gameObject.AddComponent<ObjectDrag>();
            }
            objPLaced.placed = false;
        }
    }

    public void ConfirmEdit(GameObject panel)
    {
        panel.SetActive(false);
        PlaceableObjects objPLaced = BuildingSystem.Instance.objToPlace;
        objPLaced.furnitureMode = PlaceableObjects.MODE.Putmode;
        ObjectDrag drag = objPLaced.GetComponent<ObjectDrag>();
        Destroy(drag);
        objPLaced = null;
    }
    
    public void SoldItemAfterBuy()
    {
        shopTest.ReturnMoney(furnitureGlobal);
        BuildingSystem.Instance.CancelSelectedObj();
    }
    
    
}
