using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class PlayerLook : MonoBehaviour
    {
        public float mouseSensitivity = 100f;

        public Transform playerBody;

        private float xRotation = 0f;

        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        private void Update()
        {
            if (!InterfaceManager.instance.inDialog)
            {
                Gamepad gamepad = Gamepad.current;

                if (gamepad == null) return;

                Vector2 move = gamepad.rightStick.ReadValue();

                float x = move.x * mouseSensitivity * Time.deltaTime;
                float y = move.y * mouseSensitivity * Time.deltaTime;

                xRotation -= y;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * x);
            }
        }
    }
}