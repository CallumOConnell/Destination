using UnityEngine;

namespace Destination
{
    public class Safe : InteractableBase
    {
        [Space, Header("UI Settings")]
        public GameObject safeMenu;

        private Animator animator;

        private void Start() => animator = GetComponent<Animator>();

        public override void OnInteract()
        {
            base.OnInteract();

            InterfaceManager.instance.OpenMenu("safe");
        }

        public void OpenSafe() => animator.SetBool("isOpen", true);
    }
}