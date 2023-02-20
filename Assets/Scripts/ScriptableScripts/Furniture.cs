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
    public enum FurnitureColor
    {
        RED,
        GREEN,
        WHITE,
        BLACK,
        BROWN,
    }
    public FurnitureType typeEnum;
    public FurnitureColor colorEnum;

    public GameObject furniturePrefab;
    public Sprite spriteFurniture;
    public int ArtisticValue;
    public int LuxuryValue;
    public float furniturePrice;

}
