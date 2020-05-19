using UnityEngine;

namespace Destination
{
    public class Crowbar : InteractableBase
    {
        [Space, Header("Inventory Settings")]
        public InventoryObject inventory;

        public ItemObject crowbar;

        public PickupItem pickupItem;

        public override void OnInteract()
        {
            base.OnInteract();

            inventory.AddItem(new Item(crowbar), 1);

            pickupItem.Display(crowbar.name, 1, crowbar.icon);

            gameObject.SetActive(false);
        }
    }
}