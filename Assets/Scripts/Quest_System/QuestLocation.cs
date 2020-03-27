using UnityEngine;

namespace Destination
{
    public class QuestLocation : MonoBehaviour
    {
        /*
         * This code goes on a game object that represents the
         * task to be performed by the player at the location
         * of the object. This code can contain logic as long
         * as when the task is complete it injects the three statuses
         * back into the Quest system (as per in the OnCollisionEnter)
         * currently here.
        */

        public QuestManager questManager;

        public QuestEvent questEvent;

        //public QuestButton questButton;

        public void Setup(QuestManager _questManager, QuestEvent _questEvent)
        {
            questManager = _questManager;
            questEvent = _questEvent;
            //questButton = _questButton;

            //_questEvent.button = questButton;
        }

        private void OnCollisionEnter(Collision _other)
        {
            if (!_other.gameObject.CompareTag("Player")) return;

            // If we shouldn't be working on this event then don't register it as completed.
            if (questEvent.status != QuestEvent.EventStatus.CURRENT) return;

            // Inject these back into the Quest Manager to update states.
            questEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
            //questButton.UpdateButton(QuestEvent.EventStatus.DONE);
            questManager.UpdateQuestsOnCompletion(questEvent);
        }
    }
}