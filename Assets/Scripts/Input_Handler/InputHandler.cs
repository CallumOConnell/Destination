using UnityEngine;

namespace Destination
{
    public class InputHandler : MonoBehaviour
    {
        [Space, Header("Data")]
        [SerializeField] private InteractionInputData interactionInputData = null;

        private void Start() => interactionInputData.ResetInput();

        private void Update() => GetInteractionInputData();

        private void GetInteractionInputData()
        {
            interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
            interactionInputData.InteractedReleased = Input.GetKeyUp(KeyCode.E);
        }
    }
}