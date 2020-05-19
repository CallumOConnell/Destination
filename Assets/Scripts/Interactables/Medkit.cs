using UnityEngine;

namespace Destination
{
    public class Medkit : InteractableBase
    {
        public InventoryObject inventory;

        public ItemObject medkit;

        public override void OnInteract()
        {
            base.OnInteract();

            inventory.AddItem(new Item(medkit), 1);

            Destroy(gameObject);

            FindObjectOfType<Canvas>().GetComponent<PickupItem>().Display(medkit.name, 1, medkit.icon);
        }
    }
}