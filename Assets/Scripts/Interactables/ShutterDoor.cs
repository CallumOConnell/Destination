using UnityEngine;

namespace Destination
{
    public class ShutterDoor : InteractableBase
    {
        [Space, Header("Inventory Settings")]

        public InventoryObject playerInventory;

        public ItemObject crowbar;

        [Space, Header("Audio Settings")]

        public AudioSource audioSource;

        public AudioClip audioClip;

        private Animator animator;

        private void Awake() => animator = GetComponent<Animator>();

        public override void OnInteract()
        {
            base.OnInteract();
            
            if (playerInventory.IsItemInInventory(crowbar))
            {
                animator.SetBool("isOpen", true);
            }
            else
            {
                audioSource.clip = audioClip;

                audioSource.Play();
            }
        }
    }
}