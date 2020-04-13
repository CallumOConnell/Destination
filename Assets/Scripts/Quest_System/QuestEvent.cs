using System;
using System.Collections.Generic;
using UnityEngine;

namespace Destination
{
    public class QuestEvent
    {
        public enum EventStatus { WAITING, CURRENT, DONE };

        /*
         * WAITING - Not yet completed but can't be worked on cause there's a prerequisite event.
         * CURRENT - The one the player should be trying to achieve
         * DONE - Has been archieved
        */

        public string id;
        public string name;
        public string description;

        public int order = -1;

        public EventStatus status;

        public GameObject location;

        public List<QuestPath> pathList = new List<QuestPath>();

        public Quest quest;

        public QuestEvent(string _name, string _description, GameObject _location, Quest _quest)
        {
            id = Guid.NewGuid().ToString();
            name = _name;
            description = _description;
            status = EventStatus.WAITING;
            location = _location;
            quest = _quest;
        }

        public void UpdateQuestEvent(EventStatus _newStatus) => status = _newStatus;

        public string GetID() => id;
    }
}