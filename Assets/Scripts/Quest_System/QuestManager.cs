using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Destination
{
    public class QuestManager : MonoBehaviour
    {
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
            Quest test = new Quest("Test Quest 2", "This is a second quest for testing out the quest system where the player to retrieve two blocks located around the testing area.", "Side Quest");
            
            // Define quest objectives
            // Create each event within the quest
            QuestEvent a = quest.AddQuestEvent("test1", "description 1", aLocation, quest);
            QuestEvent b = quest.AddQuestEvent("test2", "description 2", bLocation, quest);
            QuestEvent c = quest.AddQuestEvent("test3", "description 3", cLocation, quest);
            QuestEvent d = quest.AddQuestEvent("test4", "description 4", dLocation, quest);
            QuestEvent e = quest.AddQuestEvent("test5", "description 5", eLocation, quest);

            QuestEvent test1 = test.AddQuestEvent("Go to quest target block a", "description 6", aLocation, test);
            QuestEvent test2 = test.AddQuestEvent("Go to quest target block b", "description 7", bLocation, test);

            // Define the paths between the events - e.g. the order they must be completed in
            quest.AddPath(a.GetID(), b.GetID());
            quest.AddPath(b.GetID(), c.GetID());
            quest.AddPath(b.GetID(), d.GetID());
            quest.AddPath(c.GetID(), e.GetID());

            quest.BFS(a.GetID());

            test.AddPath(test1.GetID(), test2.GetID());

            test.BFS(test1.GetID());

            aLocation.GetComponent<QuestLocation>().Setup(this, a);
            bLocation.GetComponent<QuestLocation>().Setup(this, b);
            cLocation.GetComponent<QuestLocation>().Setup(this, c);
            dLocation.GetComponent<QuestLocation>().Setup(this, d);
            eLocation.GetComponent<QuestLocation>().Setup(this, e);

            aLocation.GetComponent<QuestLocation>().Setup(this, test1);
            bLocation.GetComponent<QuestLocation>().Setup(this, test2);

            quest.SetFinalEvent(e);
            test.SetFinalEvent(test2);

            // Add all quests to list
            quests.Add(quest);
            quests.Add(test);

            //quest.PrintPaths();
            //test.PrintPaths();
        }

        private IEnumerator DisplayCompletion(string _questName)
        {
            questCompletedText.text = $"{_questName} Completed!";

            questCompletedText.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            questCompletedText.gameObject.SetActive(false);
        }

        public void UpdateQuestsOnCompletion(QuestEvent _event)
        {
            if (_event == _event.quest.finalEvent)
            {
                Debug.Log($"Quest Completed Status: {_event.quest.status}");

                _event.quest.UpdateQuestStatus(Quest.QuestStatus.COMPLETED);

                Debug.Log($"Quest Completed Status: {_event.quest.status}");

                StartCoroutine(DisplayCompletion(_event.quest.name));

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