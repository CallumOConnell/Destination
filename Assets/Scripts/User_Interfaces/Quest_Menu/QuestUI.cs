using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Destination
{
    public class QuestUI : MonoBehaviour
    {
        [Space, Header("UI Settings")]
        public ListBoxControl availableQuests;
        public ListBoxControl questObjectives;

        public TextMeshProUGUI questName;
        public TextMeshProUGUI questDescription;
        public TextMeshProUGUI questType;
        public TextMeshProUGUI questStatus;

        public GameObject questUI;
        
        [Space, Header("Sprite Settings")]
        public Sprite activeQuestIcon;

        [Space, Header("Manager Settings")]
        public QuestManager questManager;

        public InputHandler inputManager;

        private CompassController compassController;

        private void Start()
        {
            compassController = GetComponent<CompassController>();

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

                    inputManager.LockControls();
                }
            }
        }

        public void UpdateUI()
        {
            List<Quest> quests = CurrentQuests();

            availableQuests.Clear();

            for (int i = 0; i < quests.Count; i++)
            {
                Quest quest = quests[i];

                if (quest.status == Quest.QuestStatus.ACTIVE)
                {
                    availableQuests.AddItem(i, quest.name, activeQuestIcon);
                }
                else if (quest.status == Quest.QuestStatus.WAITING)
                {
                    availableQuests.AddItem(i, quest.name);
                }
            }

            SelectQuest(availableQuests.gameObject, 0);
        }

        public void SelectQuest(GameObject listBox, int indexSelected)
        {
            List<Quest> quests = CurrentQuests();

            Quest selectedQuest = quests[indexSelected];

            questName.text = selectedQuest.name;
            questDescription.text = selectedQuest.description;

            if (selectedQuest.status == Quest.QuestStatus.ACTIVE)
            {
                questStatus.gameObject.SetActive(true);
            }
            else
            {
                questStatus.gameObject.SetActive(false);
            }

            if (selectedQuest.status == Quest.QuestStatus.COMPLETED)
            {
                questType.text = "";
            }
            else
            {
                questType.text = selectedQuest.type;
            }

            questObjectives.Clear();

            List<QuestEvent> objectives = selectedQuest.questEvents;

            /*
             * TODO
             * - Add functionality so that completed objectives aren't interactble and are somewhat greyed out
             *
            */

            for (int i = 0; i < objectives.Count; i++)
            {
                QuestEvent objective = objectives[i];

                if (objective.status == QuestEvent.EventStatus.CURRENT)
                {
                    questObjectives.AddItem(i, objective.name);
                }
                else if (objective.status == QuestEvent.EventStatus.DONE)
                {
                    questObjectives.AddItem(i, objective.name);
                }
            }
        }

        public void SetQuestActive(GameObject listBox, int indexSelected)
        {
            List<Quest> quests = CurrentQuests();

            foreach (Quest quest in quests)
            {
                if (quest.status == Quest.QuestStatus.ACTIVE)
                {
                    quest.UpdateQuestStatus(Quest.QuestStatus.WAITING); // Set current active quest to not active
                }
            }

            Quest selectedQuest = quests[indexSelected];

            QuestEvent currentObjective = selectedQuest.CurrentQuestEvent(selectedQuest);

            selectedQuest.UpdateQuestStatus(Quest.QuestStatus.ACTIVE);

            if (currentObjective.order == 1)
            {
                currentObjective.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            }

            UpdateUI();

            SelectQuest(listBox, indexSelected);

            questStatus.gameObject.SetActive(true);

            compassController.compassTarget = currentObjective.location;
        }

        private List<Quest> CurrentQuests() => questManager.quests;
    }
}