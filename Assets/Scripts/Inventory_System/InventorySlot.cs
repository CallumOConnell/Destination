using System;
using UnityEngine;

namespace Destination
{
    [Serializable]
    public class InventorySlot
    {
        // Fields
        [NonSerialized]
        public InventoryUI parent;
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
    }
}