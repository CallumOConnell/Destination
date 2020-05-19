using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class HealAction : MonoBehaviour
    {
        public InventoryObject inventory;

        public ItemObject medkit;

        public Timer timer;

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (gamepad.dpad.right.wasPressedThisFrame)
                {
                    StartCoroutine(ApplyMedkit());
                }
            }
        }

        private IEnumerator ApplyMedkit()
        {
            if (inventory.IsItemInInventory(medkit))
            {
                if (GetComponent<PlayerVitals>().currentHealth < GetComponent<PlayerVitals>().maxHealth)
                {
                    timer.CreateTimer(5f);

                    yield return new WaitUntil(() => !timer.timerActive);

                    inventory.RemoveItem(new Item(medkit), 1);

                    GetComponent<PlayerVitals>().ChangeHealth(25, true);
                }
            }
        }
    }
}