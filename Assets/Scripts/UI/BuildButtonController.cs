
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

    public void RotatePreviewObjButton()
    {
        BuildingSystem.Instance.RotateSelectedObj();
    }

    public void CancelSelectedObjButton()
    {
        BuildingSystem.Instance.CancelSelectedObj();
    }

    public void PlaceSelectedObjButton()
    {
        BuildingSystem.Instance.PlaceSelectedObjAndBuy();
        
        ShopSystem shopSystem = FindObjectOfType<ShopSystem>();
        shopTest.BuyFurniture(furnitureGlobal);
        shopSystem.UpdateUI();
    }
    
    
    
}
