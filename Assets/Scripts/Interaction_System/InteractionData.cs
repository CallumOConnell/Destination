using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "Interaction Data", menuName = "InteractionSystem/InteractionData")]
    public class InteractionData : ScriptableObject
    {
        private InteractableBase interactable;

        public InteractableBase Interactable
        {
            get => interactable;
            set => interactable = value;
        }

        public void Interact()
        {
            interactable.OnInteract();
            ResetData();
        }

        public bool IsSameInteractable(InteractableBase newInteractable) => interactable == newInteractable;
        public bool IsEmpty() => interactable == null;
        public void ResetData() => interactable = null;
    }
}
