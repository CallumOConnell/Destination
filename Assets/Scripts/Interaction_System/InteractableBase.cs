using UnityEngine;

namespace Destination
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        [Space, Header("Interacable Settings")]
        [SerializeField] private bool holdInteract = false;

        [SerializeField] private float holdDuration = 0f;

        [SerializeField] private bool isInteractable = true;

        [SerializeField] private string tooltip = "";

        public float HoldDuration => holdDuration;

        public bool HoldInteract => holdInteract;

        public bool IsInteractable => isInteractable;

        public string ToolTip => tooltip;

        public virtual void OnInteract() {}
    }
}