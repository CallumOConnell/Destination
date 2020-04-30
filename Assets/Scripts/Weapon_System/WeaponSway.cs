using UnityEngine;

namespace Destination
{
    public class WeaponSway : MonoBehaviour
    {
        [Space, Header("Sway Settings")]
        public float amount;
        public float maxAmount;
        public float smoothAmount;

        [Space, Header("Manager Settings")]
        public InputHandler inputManager;

        private Vector3 initialPosition;

        private void Start() => initialPosition = transform.localPosition;

        private void Update()
        {
            if (inputManager.cursorLocked)
            {
                float movementX = -Input.GetAxis("Mouse X") * amount;
                float movementY = -Input.GetAxis("Mouse Y") * amount;

                movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
                movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

                Vector3 finalPosition = new Vector3(movementX, movementY, 0);

                transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
            }
        }
    }
}