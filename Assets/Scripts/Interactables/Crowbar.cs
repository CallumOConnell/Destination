using UnityEngine;

namespace Destination
{
    public class Crowbar : InteractableBase
    {
        public InventoryObject playerInventory = null;

        public AudioSource[] voiceLine = null;

        public bool hasCrowbar = false;

        public override void OnInteract()
        {
            base.OnInteract();

            if (Item != null)
            {
                playerInventory.AddItem(Item, 1);
            }

            voiceLine[0].Play();

            hasCrowbar = true;

            gameObject.SetActive(false);
        }
    }
}