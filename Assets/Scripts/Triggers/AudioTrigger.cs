using UnityEngine;

namespace Destination
{
    public class AudioTrigger : MonoBehaviour
    {
        public AudioSource audioSource;

        public VoiceTrigger voiceTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!voiceTrigger.alreadyPlayed)
                {
                    audioSource.PlayOneShot(voiceTrigger.voiceLine);

                    voiceTrigger.alreadyPlayed = true;
                }
            }
        }
    }
}