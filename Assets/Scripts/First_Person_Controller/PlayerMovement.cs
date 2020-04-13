using UnityEngine;

namespace Destination
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;
        public float sprintSpeed = 12f;
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
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            
            /*
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical") && isGrounded && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else if (!Input.GetButtonDown("Horizontal") || !Input.GetButtonDown("Vertical") && audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            */
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            // Sprinting

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !isCrouched)
            {
                controller.Move(move * sprintSpeed * Time.deltaTime);
                //anim.SetBool("isRunning", true);
            }
            else
            {
                //anim.SetBool("isRunning", false);
            }

            // Crouching

            if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouched = true;
                controller.height = 1;
                speed = 3;
            }
            else
            {
                isCrouched = false;
                controller.height = startHeight;
                speed = 6;
            }

            // Jumping

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}