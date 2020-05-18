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

        [Space, Header("Quest Locations")]
        public GameObject questFinalLocation;

        private void Start()
        {
            // Define quest basic details
            Quest quest = new Quest("Test Quest", "First quest to complete.", "Storyline Quest");

            // Define quest objectives
            // Create each event within the quest
            QuestEvent a = quest.AddQuestEvent("test1", "description 1", questFinalLocation, quest);

            // Define the paths between the events - e.g. the order they must be completed in
            quest.AddPath(a.GetID(), a.GetID());

            quest.BFS(a.GetID());

            questFinalLocation.GetComponent<QuestLocation>().Setup(this, a);

            quest.SetFinalEvent(a);

            // Add all quests to list
            quests.Add(quest);
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
                _event.quest.UpdateQuestStatus(Quest.QuestStatus.COMPLETED);

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