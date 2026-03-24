using UnityEngine;

public enum ItemType
{
    Consumable,
    KeyItem,
    Material
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;

    public bool isStackable = true;
    public int maxStack = 10;

    public int healAmount = 0;
}