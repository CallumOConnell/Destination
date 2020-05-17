using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class FlashLight : MonoBehaviour
    {
        [Space, Header("Light Settings")]
        public Light flashLight;

        private AudioSource audioSource;
        private void Awake() => audioSource = GetComponent<AudioSource>();

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
                        audioSource.Play();
                    }
                }
            }
        }
    }
}