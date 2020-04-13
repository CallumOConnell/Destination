using System.Collections;
using UnityEngine;

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
            if (Input.GetKeyDown(KeyCode.G))
            {
                StartCoroutine(ApplyMedkit());
            }
        }

        private IEnumerator ApplyMedkit()
        {
            if (inventory.IsItemInInventory(medkit))
            {
                timer.CreateTimer(5f);

                yield return new WaitUntil(() => !timer.timerActive);

                inventory.RemoveItem(new Item(medkit), 1);

                player.GetComponent<PlayerStats>().currentHealth += 5;
            }
        }
    }
}