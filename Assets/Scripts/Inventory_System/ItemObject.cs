using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
    public class ItemObject : ScriptableObject
    {
        public Sprite icon;

        public bool stackable;

        public Item data = new Item();

        public Item CreateItem()
        {
            Item newItem = new Item(this);

            return newItem;
        }
    }
}