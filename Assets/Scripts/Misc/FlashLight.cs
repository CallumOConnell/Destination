using UnityEngine;

namespace Destination
{
    public class FlashLight : MonoBehaviour
    {
        public Light flashLight;

        public AudioSource audioSource;

        public AudioClip flashLightSound = null;
        
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