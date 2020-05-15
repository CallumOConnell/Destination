using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class InputHandler : MonoBehaviour
    {
        [Space, Header("Data Settings")]
        public InteractionInputData interactionInputData;

        [Space, Header("Script References")]
        public SwitchWeapon switchWeapon;

        private Gamepad gamepad;

        private void Start()
        {
            gamepad = Gamepad.current;

            if (gamepad == null) return;

            interactionInputData.ResetInput();
        }

        private void Update()
        {
            GetInteractionInputData();
            GetInventoryInputData();
            GetQuestInputData();
            GetSwitchWeaponInputData();
        }

        private void GetSwitchWeaponInputData()
        {
            if (!InterfaceManager.instance.inDialog)
            {
                if (gamepad.dpad.down.wasPressedThisFrame)
                {
                    switchWeapon.Switch();
                }
            }
        }

        private void GetInteractionInputData()
        {
            interactionInputData.InteractedClicked = gamepad.buttonWest.wasPressedThisFrame;
            interactionInputData.InteractedReleased = gamepad.buttonWest.wasReleasedThisFrame;
        }

        private void GetInventoryInputData()
        {
            if (gamepad.buttonNorth.wasPressedThisFrame)
            {
                InterfaceManager.instance.OpenMenu("inventory");
            }
        }

        private void GetQuestInputData()
        {
            if (gamepad.selectButton.wasPressedThisFrame)
            {
                InterfaceManager.instance.OpenMenu("quest");
            }
        }
    }
}