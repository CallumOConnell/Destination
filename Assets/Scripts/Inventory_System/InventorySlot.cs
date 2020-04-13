using System;
using UnityEngine;

namespace Destination
{
    [Serializable]
    public class InventorySlot
    {
        // Fields
        public ItemType[] AllowedItems = new ItemType[0];

        [NonSerialized]
        public UserInterface parent;
        [NonSerialized]
        public GameObject slotDisplay;

        public Item item;
        public int amount;

        // Constructor
        public InventorySlot(Item item, int amount) => UpdateSlot(item, amount);

        // Methods
        public ItemObject GetItemObject() => item.id >= 0 ? parent.inventory.database.itemObjects[item.id] : null;

        public InventorySlot() => UpdateSlot(new Item(), 0);

        public void RemoveItem() => UpdateSlot(new Item(), 0);

        public void AddAmount(int value) => UpdateSlot(item, amount += value);

        public void UpdateSlot(Item itemValue, int amountValue)
        {
            item = itemValue;
            amount = amountValue;
        }

        public bool CanPlaceInSlot(ItemObject itemObject)
        {
            if (AllowedItems.Length <= 0 || itemObject == null || itemObject.data.id < 0) return true;

            for (int i = 0; i < AllowedItems.Length; i++)
            {
                if (itemObject.type == AllowedItems[i]) return true;
            }

            return false;
        }
    }
}