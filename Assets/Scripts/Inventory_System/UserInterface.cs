using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Destination
{
    [RequireComponent(typeof(EventTrigger))]
    public class UserInterface : MonoBehaviour
    {
        [Space, Header("Inventory Settings")]

        public InventoryObject inventory;

        [Space, Header("UI Settings")]

        public GameObject panel;
        public GameObject inventoryPrefab;

        public int xStart;
        public int yStart;
        public int xSpaceBetweenItems;
        public int ySpaceBetweenItems;
        public int columns;

        private float lastClick = 0f, interval = 0.4f;

        public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        private void OnEnable()
        {
            CreateSlots();

            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                inventory.GetSlots[i].parent = this;
            }

            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(); });

            UpdateSlots();
        }

        private void Update()
        {
            if (panel.activeSelf)
            {
                UpdateSlots();
            }
        }

        public void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                GameObject obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);

                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(); });
                AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(); });

                inventory.GetSlots[i].slotDisplay = obj;

                slotsOnInterface.Add(obj, inventory.GetSlots[i]);
            }
        }

        public void UpdateSlots()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> slot in slotsOnInterface)
            {
                if (slot.Value.item.id >= 0)
                {
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.Value.GetItemObject().icon;
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1); // Increase alpha to 100%
                    slot.Key.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.amount == 1 ? string.Empty : slot.Value.amount.ToString();
                    slot.Key.transform.GetChild(1).gameObject.SetActive(true);
                    slot.Key.transform.GetChild(1).GetComponentInChildren<Button>().onClick.RemoveAllListeners(); // Not ideal solution but works for now
                    slot.Key.transform.GetChild(1).GetComponentInChildren<Button>().onClick.AddListener(RemoveItem);
                }
                else
                {
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0); // Increase alpha to 0%
                    slot.Key.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
                    slot.Key.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.slotHoveredOver = obj;
        }

        public void OnEnterInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        }

        public void OnExitInterface()
        {
            MouseData.interfaceMouseIsOver = null;
        }

        public void OnExit()
        {
            MouseData.slotHoveredOver = null;
        }

        public void OnDragStart(GameObject obj)
        {
            MouseData.tempItemBeingDragged = CreateTempItem(obj);
        }

        public void OnDragEnd(GameObject obj)
        {
            Destroy(MouseData.tempItemBeingDragged);

            if (MouseData.interfaceMouseIsOver == null) return;

            if (MouseData.slotHoveredOver)
            {
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];

                inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
            }
        }

        public void OnDrag()
        {
            if (MouseData.tempItemBeingDragged != null)
            {
                MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }

        public void OnClick(GameObject obj)
        {
            if (MouseData.interfaceMouseIsOver == null) return;

            if (MouseData.slotHoveredOver)
            {
                if (slotsOnInterface[obj].item.id >= 0)
                {
                    if ((lastClick + interval) > Time.time) // Double click 
                    {
                        InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];

                        mouseHoverSlotData.GetItemObject().Use();
                    }
                    else // Single click
                    {
                        lastClick = Time.time;
                    }
                }
            }
        }

        public void RemoveItem()
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];

            inventory.RemoveItem(mouseHoverSlotData, mouseHoverSlotData.item, 1);
        }

        // Private
        private protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();

            if (!trigger) return;

            var eventTrigger = new EventTrigger.Entry { eventID = type };

            eventTrigger.callback.AddListener(action);

            trigger.triggers.Add(eventTrigger);
        }

        private GameObject CreateTempItem(GameObject obj)
        {
            GameObject tempItem = null;

            if (slotsOnInterface[obj].item.id >= 0)
            {
                tempItem = new GameObject();

                RectTransform rt = tempItem.AddComponent<RectTransform>();

                rt.sizeDelta = new Vector2(50, 50);

                tempItem.transform.SetParent(transform.parent.parent);

                Image img = tempItem.AddComponent<Image>();

                img.sprite = slotsOnInterface[obj].GetItemObject().icon;

                img.raycastTarget = false;
            }

            return tempItem;
        }

        private Vector3 GetPosition(int i) => new Vector3(xStart + (xSpaceBetweenItems * (i % columns)), yStart + (-ySpaceBetweenItems * (i / columns)), 0f);
    }
}