using System.Collections.Generic;
using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public List<InventorySlot> container = new List<InventorySlot>();

        public void AddItem(ItemObject _item, int _amount)
        {
            bool hasItem = HasItem(_item);

            if (!hasItem)
            {
                container.Add(new InventorySlot(_item, _amount));
            }
        }

        public void RemoveItem(ItemObject _item, int _amount)
        {
            bool hasItem = HasItem(_item);

            if (hasItem)
            {
                InventorySlot slot = FindInventorySlot(container, _item);

                if (slot != null)
                {
                    int currentAmount = slot.amount;

                    if (currentAmount - _amount > 0)
                    {
                        slot.SetAmount(_amount);
                    }
                    else
                    {
                        container.Remove(slot);
                    }     
                }
            }
        }

        public bool HasItem(int _itemID)
        {
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item.id == _itemID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasItem(ItemObject _item)
        {
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item == _item)
                {
                    return true;
                }
            }

            return false;
        }

        private InventorySlot FindInventorySlot(List<InventorySlot> _inventory, ItemObject _item)
        {
            foreach (InventorySlot slot in _inventory)
            {
                if (slot.item == _item)
                {
                    return slot;
                }
            }

            return null;
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public ItemObject item;

        public int amount;

        public InventorySlot(ItemObject _item, int _amount)
        {
            item = _item;
            amount = _amount;
        }

        public void SetAmount(int _value) => amount = _value;
    }
}