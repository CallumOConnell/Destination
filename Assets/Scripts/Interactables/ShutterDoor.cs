using UnityEngine;

namespace Destination
{
    public class ShutterDoor : InteractableBase
    {
        public InventoryObject playerInventory = null;

        public AudioSource[] voiceLine = null;

        public Crowbar crowbar = null;

        private Animator animator = null;

        private void Awake() => animator = GetComponent<Animator>();

        public override void OnInteract()
        {
            base.OnInteract();

            if (crowbar.hasCrowbar)
            {
                animator.SetBool("isOpen", true);
            }
            else
            {
                voiceLine[0].Play();
            }
        }
    }
}