using System;
using System.Linq;

namespace Destination
{
    [Serializable]
    public class Inventory
    {
        public InventorySlot[] slots = new InventorySlot[24];

        public void Clear()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].item = new Item();
                slots[i].amount = 0;
            }
        }

        public bool ContainsItem(ItemObject itemObject) => Array.Find(slots, i => i.item.id == itemObject.data.id) != null;

        public bool ContainsItem(int id) => slots.FirstOrDefault(i => i.item.id == id) != null;
    }
}