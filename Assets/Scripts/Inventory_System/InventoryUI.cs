using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Destination
{
    public class InventoryUI : MonoBehaviour
    {
        [Space, Header("Inventory Settings")]
        public InventoryObject playerInventory;

        [Space, Header("Inventory UI Settings")]
        public GameObject inventoryPanel;
        public GameObject inventorySlotPrefab;

        public Transform slotHolder;

        public int xStart;
        public int yStart;
        public int xSpaceBetweenItems = 0;
        public int ySpaceBetweenItems = 0;
        public int columns = 0;

        private bool inventoryEnabled = false;

        private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

        private void Start() => CreateDisplay();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryEnabled = !inventoryEnabled;

                if (inventoryEnabled)
                {
                    inventoryPanel.SetActive(true);
                }
                else
                {
                    inventoryPanel.SetActive(false);
                }
            }

            if (inventoryPanel.activeSelf)
            {
                UpdateDisplay();
            }
        }

        public void CreateDisplay()
        {
            for (int i = 0; i < playerInventory.container.Count; i++)
            {
                // Create item
                GameObject item = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, slotHolder);
                // Set item position
                item.GetComponent<RectTransform>().localPosition = GetPosition(i);
                // Set item amount
                item.GetComponentInChildren<TextMeshProUGUI>().text = playerInventory.container[i].amount.ToString();
                // Set item icon
                item.GetComponentInChildren<Image>().sprite = playerInventory.container[i].item.icon;
                // Add item to ui dictionary
                itemsDisplayed.Add(playerInventory.container[i], item);
            }
        }

        public void UpdateDisplay()
        {
            for (int i = 0; i < playerInventory.container.Count; i++)
            {
                if (itemsDisplayed.ContainsKey(playerInventory.container[i])) // Item already exists in inventory
                {
                    itemsDisplayed[playerInventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = playerInventory.container[i].amount.ToString();
                }
                else // New item in inventory
                {
                    // Create item
                    GameObject item = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, slotHolder);
                    // Set item position
                    item.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    // Set item amount
                    item.GetComponentInChildren<TextMeshProUGUI>().text = playerInventory.container[i].amount.ToString();
                    // Set item icon
                    item.GetComponentInChildren<Image>().sprite = playerInventory.container[i].item.icon;
                    // Add item to ui dictionary
                    itemsDisplayed.Add(playerInventory.container[i], item);
                }
            }
        }

        private Vector3 GetPosition(int i)
        {
            return new Vector3(xStart + (xSpaceBetweenItems * (i % columns)), yStart + ((-ySpaceBetweenItems * (i / columns))), 0f);
        }

        // Wipes players inventory after you stop playing the game. If you want a persistant inventory you would remove this method.
        private void OnApplicationQuit() => playerInventory.container.Clear();

        // Remove Item
        // Move Item
        // Use Item

        public void RemoveItem()
        {

        }

        public void UseItem()
        {

        }
    }
}