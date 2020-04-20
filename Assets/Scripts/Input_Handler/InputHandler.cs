using UnityEngine;

namespace Destination
{
    public class InputHandler : MonoBehaviour
    {
        [Space, Header("Data Settings")]
        public InteractionInputData interactionInputData;

        [Space, Header("UI Settings")]
        public GameObject inventoryPanel;
        public GameObject questPanel;
        public GameObject hud;

        public Canvas canvas;

        [Space, Header("Script References")]
        public SwitchWeapon switchWeapon;

        public bool cursorLocked = true;

        private bool inventoryEnabled = false, questEnabled = false;

        private void Start() => interactionInputData.ResetInput();

        private void Update()
        {
            GetInteractionInputData();
            GetInventoryInputData();
            GetQuestInputData();
            GetSwitchWeaponInputData();
        }

        private void GetSwitchWeaponInputData()
        {
            if (Input.GetButtonDown("SwitchWeapon"))
            {
                switchWeapon.Switch();
            }
        }

        private void GetInteractionInputData()
        {
            interactionInputData.InteractedClicked = Input.GetButtonDown("Interaction");
            interactionInputData.InteractedReleased = Input.GetButtonUp("Interaction");
        }

        private void GetInventoryInputData()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                inventoryEnabled = !inventoryEnabled;

                if (inventoryEnabled)
                {
                    inventoryPanel.SetActive(true);

                    UnlockControls();
                }
                else
                {
                    inventoryPanel.SetActive(false);

                    LockControls();
                }
            }
        }

        private void GetQuestInputData()
        {
            if (Input.GetButtonDown("Quest"))
            {
                questEnabled = !questEnabled;

                if (questEnabled)
                {
                    questPanel.SetActive(true);

                    canvas.GetComponent<QuestUI>().UpdateUI();

                    UnlockControls();
                }
                else
                {
                    questPanel.SetActive(false);

                    LockControls();
                }
            }
        }

        public void UnlockControls()
        {
            cursorLocked = false;

            hud.SetActive(false);

            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;
        }

        public void LockControls()
        {
            cursorLocked = true;

            hud.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;
        }
    }
}