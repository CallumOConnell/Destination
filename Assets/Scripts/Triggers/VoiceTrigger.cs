using UnityEngine;

namespace Destination
{
    [CreateAssetMenu(fileName = "New Audio Trigger", menuName = "Audio Trigger System/Audio Trigger")]
    public class VoiceTrigger : ScriptableObject
    {
        public AudioClip voiceLine;

        public bool alreadyPlayed = false;
    }
}