using UnityEngine;

namespace Destination
{
    public class Inventory : MonoBehaviour
    {
        [Space, Header("Inventory Settings")]
        public InventoryObject playerInventory = null;

        [Space, Header("Inventory UI Settings")]
        public GameObject inventoryPanel = null;
        public GameObject slotHolder = null;

        private bool inventoryEnable = false;

        private int allSlots;
        private int enabledSlots;

        private GameObject[] slot;

        private void Start()
        {
            allSlots = 36;
            slot = new GameObject[allSlots];

            for (int i = 0; i < allSlots; i++)
            {
                slot[i] = slotHolder.transform.GetChild(i).gameObject;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryEnable = !inventoryEnable;
            }

            if (inventoryEnable)
            {
                inventoryPanel.SetActive(true);
            }
            else
            {
                inventoryPanel.SetActive(false);
            }
        }

        // Wipes players inventory after you stop playing the game. If you want a persistant inventory you would remove this method.
        private void OnApplicationQuit() => playerInventory.container.Clear();
    }
}