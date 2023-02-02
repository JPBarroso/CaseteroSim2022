using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Furniture Data")]
public class Furniture : ScriptableObject
{
    public string furnitureName;
    public enum FurnitureType
    {
        CHAIR,
        TABLE,
        WALL,
        BAR,
        ELECTRONICS,
    }
    public FurnitureType typeEnum;
    
    public GameObject furniturePrefab;
    public Sprite spriteFurniture;
    public float furniturePrice;

}
