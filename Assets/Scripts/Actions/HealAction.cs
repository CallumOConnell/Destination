using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class HealAction : MonoBehaviour
    {
        public InventoryObject inventory;

        public ItemObject medkit;

        public GameObject player;

        private Timer timer;

        private void Start() => timer = GetComponent<Timer>();

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
                if (player.GetComponent<PlayerVitals>().currentHealth < player.GetComponent<PlayerVitals>().maxHealth)
                {
                    timer.CreateTimer(5f);

                    yield return new WaitUntil(() => !timer.timerActive);

                    inventory.RemoveItem(new Item(medkit), 1);

                    player.GetComponent<PlayerVitals>().ChangeHealth(25, true);
                }
            }
        }
    }
}