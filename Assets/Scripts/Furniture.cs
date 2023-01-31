using UnityEngine;

[System.Serializable]
public class Furniture
{
    public string furnitureName;
    public enum Maker
    {
        SEBILA,
        ERBETI,
        ALBA,
        SANPABLO,
        GUADALQUIVIR,
        TRIANA,
        CAMAS,
        PALOMA,
        MDOSIS,
        KORA,
        SERVA
    }
    public Maker makerEnum;
    
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

    public Furniture(string name, Maker maker, FurnitureType type, GameObject prefab, Sprite sprite, float value)
    {
        furnitureName = name;
        makerEnum = maker;
        typeEnum = type;
        furniturePrefab = prefab;
        spriteFurniture = sprite;
        furniturePrice = value;
    }

}
