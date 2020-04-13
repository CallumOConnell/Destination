using UnityEngine;

namespace Destination
{
    public class Crowbar : InteractableBase
    {
        [Space, Header("Audio Settings")]
        public InventoryObject inventory;

        public ItemObject crowbar;

        [Space, Header("Audio Settings")]
        public AudioSource[] voiceLine;

        public override void OnInteract()
        {
            base.OnInteract();

            inventory.AddItem(new Item(crowbar), 1);

            voiceLine[0].Play();

            gameObject.SetActive(false);
        }
    }
}