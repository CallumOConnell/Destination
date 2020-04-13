using UnityEngine;

namespace Destination
{
    public class PlayerLook : MonoBehaviour
    {
        public float mouseSensitivity = 100f;

        public Transform playerBody;

        public InputHandler inputHandler;

        private float xRotation = 0f;

        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        private void Update()
        {
            if (inputHandler.cursorLocked)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
    }
}