using UnityEngine;

namespace Destination
{
    public class PlayerAudio : MonoBehaviour
    {
        private CharacterController characterController;
        private AudioSource audioSource;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (characterController.isGrounded == true && characterController.velocity.x > 0.1f && characterController.velocity.z > 0.1f && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}