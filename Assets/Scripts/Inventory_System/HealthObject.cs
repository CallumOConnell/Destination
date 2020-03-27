using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "New Health Object", menuName = "Inventory System/Items/Health")]
    public class HealthObject : ItemObject
    {
        public int restoreHealthValue;

        public void Awake()
        {
            type = ItemType.Health;
        }
    }
}