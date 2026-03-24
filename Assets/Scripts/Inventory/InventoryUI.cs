using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Image[] slotIcons;
    public TMP_Text[] slotQuantities;
    public Image[] slotHighlights;

    public void RefreshUI(InventorySlot[] slots, int selectedSlotIndex)
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            if (i < slots.Length && slots[i] != null && !slots[i].IsEmpty())
            {
                slotIcons[i].sprite = slots[i].itemData.icon;
                slotIcons[i].enabled = true;

                if (slotQuantities != null && i < slotQuantities.Length)
                {
                    slotQuantities[i].text = slots[i].quantity.ToString();
                }
            }
            else
            {
                slotIcons[i].sprite = null;
                slotIcons[i].enabled = false;

                if (slotQuantities != null && i < slotQuantities.Length)
                {
                    slotQuantities[i].text = "";
                }
            }

            if (slotHighlights != null && i < slotHighlights.Length)
            {
                slotHighlights[i].enabled = (i == selectedSlotIndex);
            }
        }
    }
}