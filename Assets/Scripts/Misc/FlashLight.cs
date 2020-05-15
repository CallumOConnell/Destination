using UnityEngine;
using UnityEngine.InputSystem;

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
            Gamepad gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (gamepad.dpad.up.wasPressedThisFrame)
                {
                    if (!InterfaceManager.instance.inDialog)
                    {
                        flashLight.enabled = !flashLight.enabled;
                        audioSource.PlayOneShot(flashLightSound);
                    }
                }
            }
        }
    }
}