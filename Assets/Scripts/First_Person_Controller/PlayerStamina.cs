using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Destination
{
    public class PlayerStamina : MonoBehaviour
    {
        public Slider staminaBar;

        public int maxStamina = 100;
        public int staminaFallRate = 1;
        public int staminaFallMultiplier = 5;
        public int staminaRegenRate = 1;
        public int staminaRegenMultiplier = 5;

        private float currentStamina = 0;

        private PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();

            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad == null) return;

            Vector2 moveAxis = gamepad.leftStick.ReadValue();

            float x = moveAxis.x;
            float y = moveAxis.y;

            bool isMoving = x > 0 || x < 0 || y > 0 || y < 0;

            if (isMoving && gamepad.leftStickButton.isPressed)
            {
                staminaBar.value -= Time.deltaTime / staminaFallRate * staminaFallMultiplier;
                currentStamina -= Time.deltaTime / staminaFallRate * staminaFallMultiplier;
            }
            else
            {
                staminaBar.value += Time.deltaTime / staminaRegenRate * staminaRegenMultiplier;
                currentStamina += Time.deltaTime / staminaRegenRate * staminaRegenMultiplier;
            }

            if (currentStamina >= maxStamina)
            {
                staminaBar.value = maxStamina;

                currentStamina = maxStamina;
            }
            else if (currentStamina <= 0)
            {
                staminaBar.value = 0;
                currentStamina = 0;

                playerMovement.sprintSpeed = playerMovement.walkSpeed;
            }
            else if (currentStamina >= 0)
            {
                playerMovement.sprintSpeed = playerMovement.sprintSpeedNormal;
            }
        }
    }
}