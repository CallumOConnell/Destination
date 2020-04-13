using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
    public class ItemObject : ScriptableObject
    {
        public Sprite icon;

        public bool stackable;

        public ItemType type;

        [TextArea(15, 20)] public string description;

        public Item data = new Item();

        public Item CreateItem()
        {
            Item newItem = new Item(this);

            return newItem;
        }

        public virtual void Use()
        {
            Debug.Log($"Used: {name}");
        }
    }
}