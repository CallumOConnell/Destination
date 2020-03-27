using UnityEngine;

namespace Destination
{
    public class Safe : InteractableBase
    {
        public bool isUnlocked = false;

        public GameObject safeCodeUI = null;

        private Animator animator = null;

        private void Start() => animator = GetComponent<Animator>();

        public override void OnInteract()
        {
            base.OnInteract();

            safeCodeUI.SetActive(true);
        }

        public void OpenSafe() => animator.SetBool("Safe Open", true);
    }
}