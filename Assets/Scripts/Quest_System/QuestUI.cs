using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Destination
{
    public class QuestUI : MonoBehaviour
    {
        public ListBoxControl availableQuests;
        public ListBoxControl questObjectives;

        public TextMeshProUGUI questType;
        public TextMeshProUGUI questStatus;

        public Sprite activeQuestIcon;

        public GameObject manager;
        public GameObject questUI;

        private QuestManager questManager;

        private CompassController compassController;

        private void Awake() => compassController = GetComponent<CompassController>();

        private void Start()
        {
            questManager = manager.GetComponent<QuestManager>();

            availableQuests.OnChange += SelectQuest;
            availableQuests.OnDoubleClick += SetQuestActive;
        }

        private void Update()
        {
            if (questUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    questUI.SetActive(false);
                }
            }
        }

        public void UpdateUI()
        {
            List<Quest> quests = questManager.quests;

            for (int i = 0; i < quests.Count; i++)
            {
                Quest quest = quests[i];

                if (quest.status == Quest.QuestStatus.ACTIVE)
                {
                    availableQuests.AddItem(i, quest.name, activeQuestIcon);
                }
                else
                {
                    availableQuests.AddItem(i, quest.name);
                }
            }
        }

        public void SelectQuest(GameObject listBox, int indexSelected)
        {
            List<Quest> quests = CurrentQuests();

            Quest selectedQuest = quests[indexSelected];

            if (selectedQuest.status == Quest.QuestStatus.ACTIVE)
            {
                questStatus.gameObject.SetActive(true);
            }

            if (selectedQuest.status == Quest.QuestStatus.COMPLETED)
            {
                questType.text = "";
            }
            else
            {
                questType.text = selectedQuest.type;
            }

            List<QuestEvent> objectives = selectedQuest.questEvents;

            for (int i = 0; i < objectives.Count; i++)
            {
                QuestEvent objective = objectives[i];

                questObjectives.AddItem(i, objective.name);
            }

            questObjectives.AddItem((objectives.Count + 1), selectedQuest.description);
        }

        public void SetQuestActive(GameObject listBox, int indexSelected)
        {
            List<Quest> quests = CurrentQuests();

            Quest selectedQuest = quests[indexSelected];

            QuestEvent currentObjective = selectedQuest.CurrentQuestEvent(selectedQuest);

            selectedQuest.UpdateQuestStatus(Quest.QuestStatus.ACTIVE);

            compassController.target = currentObjective.location;
        }

        private List<Quest> CurrentQuests() => questManager.quests;
    }
}