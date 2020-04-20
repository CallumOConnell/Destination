using UnityEngine;

namespace Destination
{
    public class FlashLight : MonoBehaviour
    {
        [Space, Header("Light Settings")]
        public Light flashLight;

        [Space, Header("Audio Settings")]
        public AudioSource audioSource;

        public AudioClip flashLightSound;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                flashLight.enabled = !flashLight.enabled;
                audioSource.PlayOneShot(flashLightSound);
            }
        }
    }
}