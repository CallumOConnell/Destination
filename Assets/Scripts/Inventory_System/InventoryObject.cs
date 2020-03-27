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

        public void AddAmount(int _value) => amount = _value;
    }
}