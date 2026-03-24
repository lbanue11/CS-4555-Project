using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots = new InventorySlot[5];
    public int selectedSlotIndex = 0;

    public InventoryUI inventoryUI;
    public PlayerStats playerStats;

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = new InventorySlot();
            }
        }

        UpdateUI();
    }

    private void Update()
    {
        HandleSlotSelection();

        if (Input.GetKeyDown(KeyCode.U))
        {
            UseSelectedItem();
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        if (item == null || amount <= 0)
        {
            return false;
        }

        if (item.isStackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].IsEmpty() && slots[i].itemData == item)
                {
                    if (slots[i].quantity < item.maxStack)
                    {
                        int spaceLeft = item.maxStack - slots[i].quantity;
                        int amountToAdd = Mathf.Min(spaceLeft, amount);

                        slots[i].quantity += amountToAdd;
                        amount -= amountToAdd;

                        Debug.Log("Added " + item.itemName + " to slot " + i + ". Quantity is now " + slots[i].quantity);

                        if (amount <= 0)
                        {
                            UpdateUI();
                            return true;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty())
            {
                slots[i].itemData = item;

                if (item.isStackable)
                {
                    int amountToPlace = Mathf.Min(item.maxStack, amount);
                    slots[i].quantity = amountToPlace;
                    amount -= amountToPlace;
                }
                else
                {
                    slots[i].quantity = 1;
                    amount -= 1;
                }

                Debug.Log("Added " + item.itemName + " to empty slot " + i + ". Quantity is now " + slots[i].quantity);

                if (amount <= 0)
                {
                    UpdateUI();
                    return true;
                }
            }
        }

        Debug.Log("Inventory full. Could not add " + item.itemName);
        UpdateUI();
        return false;
    }

    public void UseSelectedItem()
    {
        Debug.Log("Use key pressed. Selected slot index: " + selectedSlotIndex);

        if (selectedSlotIndex < 0 || selectedSlotIndex >= slots.Length)
        {
            Debug.Log("Selected slot index is out of range.");
            return;
        }

        InventorySlot slot = slots[selectedSlotIndex];

        if (slot == null)
        {
            Debug.Log("Selected slot is null.");
            return;
        }

        if (slot.IsEmpty())
        {
            Debug.Log("No item in selected slot.");
            return;
        }

        ItemData item = slot.itemData;

        if (item == null)
        {
            Debug.Log("Item data is null.");
            return;
        }

        Debug.Log("Trying to use item: " + item.itemName);

        if (item.itemType == ItemType.Consumable)
        {
            if (item.healAmount > 0 && playerStats != null)
            {
                playerStats.Heal(item.healAmount);
                RemoveOneFromSlot(selectedSlotIndex);
                Debug.Log("Used " + item.itemName);
            }
            else
            {
                Debug.Log("Consumable item found, but heal amount is 0 or PlayerStats is missing.");
            }
        }
        else
        {
            Debug.Log(item.itemName + " cannot be used right now.");
        }

        UpdateUI();
    }

    public void RemoveOneFromSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
        {
            return;
        }

        if (slots[slotIndex].IsEmpty())
        {
            return;
        }

        slots[slotIndex].quantity--;

        if (slots[slotIndex].quantity <= 0)
        {
            slots[slotIndex].ClearSlot();
        }

        UpdateUI();
    }

    private void HandleSlotSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSlotIndex = 0;
            Debug.Log("Selected slot 1");
            UpdateUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && slots.Length > 1)
        {
            selectedSlotIndex = 1;
            Debug.Log("Selected slot 2");
            UpdateUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && slots.Length > 2)
        {
            selectedSlotIndex = 2;
            Debug.Log("Selected slot 3");
            UpdateUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && slots.Length > 3)
        {
            selectedSlotIndex = 3;
            Debug.Log("Selected slot 4");
            UpdateUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && slots.Length > 4)
        {
            selectedSlotIndex = 4;
            Debug.Log("Selected slot 5");
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.RefreshUI(slots, selectedSlotIndex);
        }
    }
}