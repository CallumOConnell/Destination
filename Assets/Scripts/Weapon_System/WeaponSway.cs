using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class WeaponSway : MonoBehaviour
    {
        [Space, Header("Sway Settings")]
        public float amount;
        public float maxAmount;
        public float smoothAmount;

        private Vector3 initialPosition;

        private void Start() => initialPosition = transform.localPosition;

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (!InterfaceManager.instance.inDialog)
                {
                    Vector2 move = gamepad.rightStick.ReadValue();

                    float x = move.x * amount;
                    float y = move.y * amount;

                    x = Mathf.Clamp(x, -maxAmount, maxAmount);
                    y = Mathf.Clamp(y, -maxAmount, maxAmount);

                    Vector3 finalPosition = new Vector3(x, y, 0);

                    transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
                }
            }
        }
    }
}