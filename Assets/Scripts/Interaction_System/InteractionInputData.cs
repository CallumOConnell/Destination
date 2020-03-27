using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "Interaction Input Data", menuName = "InteractionSystem/InputData")]
    public class InteractionInputData : ScriptableObject
    {
        private bool interactedClicked;
        private bool interactedReleased;

        public bool InteractedClicked
        {
            get => interactedClicked;
            set => interactedClicked = value;
        }

        public bool InteractedReleased
        {
            get => interactedReleased;
            set => interactedReleased = value;
        }

        public void ResetInput()
        {
            interactedClicked = false;
            interactedReleased = false;
        }
    }
}