using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Database")]
    public class ItemDatabaseObject : ScriptableObject
    {
        public ItemObject[] itemObjects;

        public void OnValidate()
        {
            for (int i = 0; i < itemObjects.Length; i++)
            {
                itemObjects[i].data.id = i;
            }
        }
    }
}