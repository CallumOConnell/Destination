using UnityEngine;

namespace Destination
{
    public class VoiceTrigger : MonoBehaviour
    {
        public AudioSource[] voiceLine = null;

        private bool alreadyPlayed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!alreadyPlayed)
                {
                    voiceLine[0].Play();

                    alreadyPlayed = true;
                }
            }
        }
    }
}