using UnityEngine;

namespace Destination
{
    public class Safe : InteractableBase
    {
        [Space, Header("UI Settings")]
        public GameObject safeMenu;

        public InputHandler inputHandler;

        private Animator animator;

        private void Start() => animator = GetComponent<Animator>();

        public override void OnInteract()
        {
            base.OnInteract();

            inputHandler.UnlockControls();

            safeMenu.SetActive(true);
        }

        public void OpenSafe() => animator.SetBool("isOpen", true);
    }
}