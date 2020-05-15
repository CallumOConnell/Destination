using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 2f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;
        public float sprintSpeed = 3f;
        public float sprintSpeedNormal = 3f;
        public float groundDistance = 0.4f;

        public Transform groundCheck;
        
        public LayerMask groundMask = ~0;

        private float startHeight;

        private Vector3 velocity;

        private bool isGrounded;
        private bool isCrouched = false;

        private CharacterController controller;

        private AudioSource audioSource;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            audioSource = GetComponent<AudioSource>();

            startHeight = controller.height;
        }

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad == null) return;

            if (!InterfaceManager.instance.inDialog)
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }

                Vector2 moveAxis = gamepad.leftStick.ReadValue();

                float x = moveAxis.x;
                float z = moveAxis.y;

                bool isMoving = x > 0 || x < 0 || z > 0 || z < 0;

                if (isMoving && isGrounded && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                else if (!isMoving && audioSource.isPlaying)
                {
                    audioSource.Pause();
                }

                Vector3 move = transform.right * x + transform.forward * z;

                controller.Move(move * speed * Time.deltaTime);

                // Sprinting

                if (gamepad.leftStickButton.isPressed && isGrounded && !isCrouched)
                {
                    controller.Move(move * sprintSpeed * Time.deltaTime);
                }

                // Crouching

                if (gamepad.buttonEast.wasPressedThisFrame)
                {
                    if (isCrouched)
                    {
                        isCrouched = false;
                        controller.height = startHeight;
                        speed = 2;
                    }
                    else
                    {
                        isCrouched = true;
                        controller.height = 1;
                        speed = 1;
                    }
                }

                // Jumping

                if (gamepad.buttonSouth.wasPressedThisFrame && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);
            }
        }
    }
}