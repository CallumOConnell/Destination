using UnityEngine;
using UnityEngine.UI;

namespace Destination
{
    public class QuestButton : MonoBehaviour
    {
        public Button buttonComponent;

        public RawImage icon;

        public Text eventName;

        public Sprite currentImage;
        public Sprite waitingImage;
        public Sprite doneImage;

        public QuestEvent thisEvent;

        public CompassController compassController;

        private QuestEvent.EventStatus status;

        private void Awake()
        {
            buttonComponent.onClick.AddListener(ClickHandler);
            compassController = GameObject.Find("Canvas").GetComponent<CompassController>();
        }

        public void Setup(QuestEvent _event, GameObject _scrollList)
        {
            thisEvent = _event;
            buttonComponent.transform.SetParent(_scrollList.transform, false);
            eventName.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.description;
            status = thisEvent.status;
            icon.texture = waitingImage.texture;
            buttonComponent.interactable = false;
        }

        public void UpdateButton(QuestEvent.EventStatus _status)
        {
            status = _status;

            if (status == QuestEvent.EventStatus.DONE)
            {
                icon.texture = doneImage.texture;
                buttonComponent.interactable = false;
            }
            else if (status == QuestEvent.EventStatus.WAITING)
            {
                icon.texture = waitingImage.texture;
                buttonComponent.interactable = false;
            }
            else if (status == QuestEvent.EventStatus.CURRENT)
            {
                icon.texture = currentImage.texture;
                buttonComponent.interactable = true;
                ClickHandler();
            }
        }

        public void ClickHandler() => compassController.target = thisEvent.location; // Set compass controller to point toward the location of this event
    }
}