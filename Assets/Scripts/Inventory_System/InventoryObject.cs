using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        // Fields
        public string savePath;

        public ItemDatabaseObject database;

        [SerializeField]
        private Inventory container = new Inventory();

        // Properties
        public InventorySlot[] GetSlots => container.slots;

        public int EmptySlotCount
        {
            get
            {
                int counter = 0;

                for (int i = 0; i < GetSlots.Length; i++)
                {
                    if (GetSlots[i].item.id <= -1)
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }

        // Methods
        public bool AddItem(Item item, int amount)
        {
            if (EmptySlotCount <= 0) return false;

            InventorySlot slot = FindItemOnInventory(item);

            if (!database.itemObjects[item.id].stackable || slot == null)
            {
                GetEmptySlot().UpdateSlot(item, amount);
                return true;
            }

            slot.AddAmount(amount);

            return true;
        }

        public void RemoveItem(InventorySlot slot, Item _item, int _amount)
        {
            if (slot != null)
            {
                int currentAmount = slot.amount;
                int newAmount = currentAmount - _amount;

                if (newAmount > 0)
                {
                    slot.UpdateSlot(_item, newAmount);
                }
                else
                {
                    slot.RemoveItem();
                }
            }
        }

        public void RemoveItem(Item _item, int _amount)
        {
            InventorySlot slot = FindItemOnInventory(_item);

            int currentAmount = slot.amount;
            int newAmount = currentAmount - _amount;

            if (newAmount > 0)
            {
                slot.UpdateSlot(_item, newAmount);
            }
            else
            {
                slot.RemoveItem();
            }
        }

        public InventorySlot FindItemOnInventory(Item item)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.id == item.id)
                {
                    return GetSlots[i];
                }
            }

            return null;
        }

        public bool IsItemInInventory(ItemObject item)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.id == item.data.id)
                {
                    return true;
                }
            }

            return false;
        }

        public InventorySlot GetEmptySlot()
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.id <= -1)
                {
                    return GetSlots[i];
                }
            }

            return null;
        }

        public void SwapItems(InventorySlot item1, InventorySlot item2)
        {
            if (item1 == item2) return;

            if (item2.CanPlaceInSlot(item1.GetItemObject()) && item1.CanPlaceInSlot(item2.GetItemObject()))
            {
                InventorySlot temp = new InventorySlot(item2.item, item2.amount);

                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
            }
        }

        [ContextMenu("Save")]
        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, container);

            stream.Close();
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                IFormatter formatter = new BinaryFormatter();

                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);

                Inventory newContainer = (Inventory)formatter.Deserialize(stream);

                for (int i = 0; i < GetSlots.Length; i++)
                {
                    GetSlots[i].UpdateSlot(newContainer.slots[i].item, newContainer.slots[i].amount);
                }

                stream.Close();
            }
        }

        [ContextMenu("Clear")]
        public void Clear() => container.Clear();
    }
}