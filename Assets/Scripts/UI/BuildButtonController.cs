
using UnityEngine;

public class BuildButtonController : MonoBehaviour
{
    
    public void BuildPreviewObjectButton(GameObject selectPrefab)
    {
        BuildingSystem.Instance.PreviewSelectedObj(selectPrefab);
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
