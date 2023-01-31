
using UnityEngine;

public class BuildButtonController : MonoBehaviour
{
    
    public void BuildPreviewObjectButton(Furniture furniture)
    {
        BuildingSystem.Instance.PreviewSelectedObj(furniture);
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
    }
    
    
    
}
