using UnityEngine;

namespace Destination
{
    public class Crowbar : InteractableBase
    {
        [Space, Header("Inventory Settings")]
        public InventoryObject inventory;

        public ItemObject crowbar;

        public PickupItem pickupItem;

        [Space, Header("Audio Settings")]
        public AudioSource audioSource;

        public AudioClip audioClip;

        public override void OnInteract()
        {
            base.OnInteract();

            inventory.AddItem(new Item(crowbar), 1);

            audioSource.PlayOneShot(audioClip);

            pickupItem.Display(crowbar.name, 1, crowbar.icon);

            gameObject.SetActive(false);
        }
    }
}