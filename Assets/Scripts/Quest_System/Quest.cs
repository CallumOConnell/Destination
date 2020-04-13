using System;
using System.Collections.Generic;
using UnityEngine;

namespace Destination
{
    public class Quest
    {
        public enum QuestStatus { WAITING, ACTIVE, COMPLETED };

        /*
         * WAITING - Quest can be completed but player hasn't completed the quest completetion condition
         * ACTIVE - Player is currently tracking this quest
         * COMPLETED - Quest has been achieved and archieved
        */

        public string id;
        public string name;
        public string description;
        public string type;

        public QuestStatus status;

        public List<QuestEvent> questEvents = new List<QuestEvent>();
        
        public QuestEvent finalEvent;

        public Quest(string _name, string _description, string _type)
        {
            id = Guid.NewGuid().ToString();
            name = _name;
            description = _description;
            type = _type;
            status = QuestStatus.WAITING;
        }

        public void SetFinalEvent(QuestEvent _finalEvent) => finalEvent = _finalEvent;

        public void UpdateQuestStatus(QuestStatus _newStatus) => status = _newStatus;

        public QuestEvent CurrentQuestEvent(Quest _quest)
        {
            foreach (QuestEvent questEvent in _quest.questEvents)
            {
                if (questEvent.status == QuestEvent.EventStatus.CURRENT)
                {
                    return questEvent;
                }
            }

            return null;
        }

        public bool IsQuestCompleted(Quest _quest)
        {
            foreach (QuestEvent questEvent in _quest.questEvents)
            {
                if (questEvent.status == QuestEvent.EventStatus.WAITING) // If any one of the quest objectives aren't done then the quest isn't complete
                {
                    return false;
                }
            }

            return true;
        }

        public QuestEvent AddQuestEvent(string _name, string _description, GameObject _location, Quest _quest)
        {
            QuestEvent questEvent = new QuestEvent(_name, _description, _location, _quest);

            questEvents.Add(questEvent);

            return questEvent;
        }

        public void AddPath(string _fromQuestEvent, string _toQuestEvent)
        {
            QuestEvent from = FindQuestEvent(_fromQuestEvent);
            QuestEvent to = FindQuestEvent(_toQuestEvent);

            if (from != null && to != null)
            {
                QuestPath path = new QuestPath(from, to);

                from.pathList.Add(path);
            }
        }

        private QuestEvent FindQuestEvent(string _id)
        {
            foreach (QuestEvent questEvent in questEvents)
            {
                if (questEvent.GetID() == _id)
                {
                    return questEvent;
                }
            }

            return null;
        }

        public void BFS(string _id, int _orderNumber = 1)
        {
            QuestEvent thisEvent = FindQuestEvent(_id);

            if (thisEvent != null)
            {
                thisEvent.order = _orderNumber;

                if (thisEvent.order == 1)
                {
                    thisEvent.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                }

                foreach (QuestPath e in thisEvent.pathList)
                {
                    if (e.endEvent.order == -1)
                    {
                        BFS(e.endEvent.GetID(), _orderNumber + 1);
                    }
                }
            }
            else
            {
                Debug.LogError($"Couldn't find quest event with id of {_id}");
            }
        }

        public void PrintPaths()
        {
            foreach (QuestEvent _event in questEvents)
            {
                Debug.Log($"Name: {_event.name} Order: {_event.order} Status: {_event.status}");
            }
        }
    }
}