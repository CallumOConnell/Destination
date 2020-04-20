using UnityEngine;

namespace Destination
{
    public class AudioManager : MonoBehaviour
    {
        public VoiceTrigger[] voiceTriggers;

        public void ResetTriggers()
        {
            foreach (VoiceTrigger trigger in voiceTriggers)
            {
                trigger.alreadyPlayed = false;
            }
        }
    }
}