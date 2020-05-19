using UnityEngine;

namespace Destination
{
    public class InteractionController : MonoBehaviour
    {
        [Space, Header("Data")]
        [SerializeField] private InteractionInputData interactionInputData = null;
        [SerializeField] private InteractionData interactionData = null;

        [Space, Header("Ray Settings")]
        [SerializeField] private float rayDistance = 0f;
        [SerializeField] private float raySphereRadius = 0f;
        [SerializeField] private LayerMask interactableLayer = ~0;

        [Space, Header("UI Settings")]
        public Canvas canvas;

        [Space, Header("Camera Settings")]
        public Camera mainCamera;

        private InteractionUI interactionUI;

        private bool interacting;

        private float holdTimer = 0f;

        private float holdDuration = 0f;

        private void Awake() => interactionUI = canvas.GetComponent<InteractionUI>();

        private void Update()
        {
            CheckForInteractable();
            CheckForInteractableInput();
        }

        private void CheckForInteractable()
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

            bool hitSomething = Physics.SphereCast(ray, raySphereRadius, out RaycastHit hitInfo, rayDistance, interactableLayer);

            if (hitSomething)
            {
                InteractableBase interactable = hitInfo.transform.GetComponent<InteractableBase>();

                interactionUI.SetToolTip(interactable.ToolTip);
                interactionUI.SetTooltipActiveState(true);

                if (interactable != null)
                {
                    if (interactionData.IsEmpty())
                    {
                        interactionData.Interactable = interactable;
                    }
                    else
                    {
                        if (!interactionData.IsSameInteractable(interactable))
                        {
                            interactionData.Interactable = interactable;
                        }
                    }
                }
            }
            else
            {
                interactionData.ResetData();

                interactionUI.SetTooltipActiveState(false);
            }

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);
        }

        private void CheckForInteractableInput()
        {
            if (interactionData.IsEmpty()) return;

            if (interactionInputData.InteractedClicked)
            {
                interacting = true;
                holdTimer = 0f;
            }

            if (interactionInputData.InteractedReleased)
            {
                interacting = false;
                holdTimer = 0f;
            }

            if (interacting)
            {
                if (!interactionData.Interactable.IsInteractable) return;

                if (interactionData.Interactable.HoldInteract)
                {
                    holdTimer += Time.deltaTime;

                    holdDuration = interactionData.Interactable.HoldDuration;

                    if (holdTimer >= holdDuration)
                    {
                        interactionData.Interact();
                        interacting = false;
                    }
                }
                else
                {
                    interactionData.Interact();
                    interacting = false;
                }
            }

            if (interactionData.Interactable != null && interactionData.Interactable.HoldInteract)
            {
                interactionUI.UpdateChargeProgress(holdTimer / holdDuration);
            }
        }
    }
}