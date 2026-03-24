using UnityEngine;

public class WorldItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public int quantity = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger hit by: " + other.name);

        Inventory inventory = other.GetComponentInParent<Inventory>();

        if (inventory != null)
        {
            Debug.Log("Inventory FOUND");

            bool wasAdded = inventory.AddItem(itemData, quantity);

            if (wasAdded)
            {
                Debug.Log("Picked up " + itemData.itemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory full.");
            }
        }
        else
        {
            Debug.Log("Inventory NOT found");
        }
    }
}