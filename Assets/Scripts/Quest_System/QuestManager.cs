using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Destination
{
    public class QuestManager : MonoBehaviour
    {
        [HideInInspector]
        public List<Quest> quests = new List<Quest>();

        [Space, Header("UI Settings")]
        public GameObject questUI;

        public TextMeshProUGUI questCompletedText;

        public GameObject aLocation;
        public GameObject bLocation;
        public GameObject cLocation;
        public GameObject dLocation;
        public GameObject eLocation;

        private void Start()
        {
            // Define quest basic details
            Quest quest = new Quest("Test Quest", "First quest to complete.", "Storyline Quest");

            
            // Define quest objectives
            // Create each event within the quest
            QuestEvent a = quest.AddQuestEvent("test1", "description 1", aLocation, quest);
            QuestEvent b = quest.AddQuestEvent("test2", "description 2", bLocation, quest);
            QuestEvent c = quest.AddQuestEvent("test3", "description 3", cLocation, quest);
            QuestEvent d = quest.AddQuestEvent("test4", "description 4", dLocation, quest);
            QuestEvent e = quest.AddQuestEvent("test5", "description 5", eLocation, quest);

            // Define the paths between the events - e.g. the order they must be completed in
            quest.AddPath(a.GetID(), b.GetID());
            quest.AddPath(b.GetID(), c.GetID());
            quest.AddPath(b.GetID(), d.GetID());
            quest.AddPath(c.GetID(), e.GetID());

            quest.BFS(a.GetID());

            //QuestButton button = CreateButton(a).GetComponent<QuestButton>();

            aLocation.GetComponent<QuestLocation>().Setup(this, a);
            bLocation.GetComponent<QuestLocation>().Setup(this, b);
            cLocation.GetComponent<QuestLocation>().Setup(this, c);
            dLocation.GetComponent<QuestLocation>().Setup(this, d);
            eLocation.GetComponent<QuestLocation>().Setup(this, e);

            // Add all quests to list
            quests.Add(quest);

            // Debug
            //quest.PrintPath();
        }
        /*
        private GameObject CreateButton(QuestEvent _event)
        {
            GameObject button = Instantiate(buttonPrefab);

            button.GetComponent<QuestButton>().Setup(_event, questPrintBox);

            if (_event.order == 1)
            {
                button.GetComponent<QuestButton>().UpdateButton(QuestEvent.EventStatus.CURRENT);
                _event.status = QuestEvent.EventStatus.CURRENT;
            }

            return button;
        }
        */
        private IEnumerator DisplayCompletion()
        {
            questCompletedText.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            questCompletedText.gameObject.SetActive(false);
        }

        public void UpdateQuestsOnCompletion(QuestEvent _event)
        {
            if (_event == _event.quest.finalEvent)
            {
                StartCoroutine(DisplayCompletion());

                return;
            }

            foreach (QuestEvent n in _event.quest.questEvents)
            {
                if (n.order == (_event.order + 1))
                {
                    // Make the next in line available for completion
                    n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                }
            }
        }
    }
}