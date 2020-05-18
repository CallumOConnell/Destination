using UnityEngine;

namespace Destination
{
    public class Medkit : InteractableBase
    {
        public InventoryObject inventory;

        public ItemObject medkit;

        public PickupItem pickupItem;

        public override void OnInteract()
        {
            base.OnInteract();

            inventory.AddItem(new Item(medkit), 1);

            Destroy(gameObject);

            pickupItem.Display(medkit.name, 1, medkit.icon);
        }
    }
}