using System;

[Serializable]
public class InventorySlot
{
    public ItemData itemData;
    public int quantity;

    public bool IsEmpty()
    {
        return itemData == null || quantity <= 0;
    }

    public void ClearSlot()
    {
        itemData = null;
        quantity = 0;
    }
}