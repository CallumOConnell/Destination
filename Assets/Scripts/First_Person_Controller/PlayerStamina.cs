using UnityEngine;
using UnityEngine.UI;

namespace Destination
{
    public class PlayerStamina : MonoBehaviour
    {
        public Slider staminaBar;

        public CharacterController controller;

        public PlayerMovement playerMovement;

        public int maxStamina = 100;
        public int staminaFallRate = 1;
        public int staminaFallMultiplier = 5;
        public int staminaRegenRate = 1;
        public int staminaRegenMultiplier = 5;

        private void Start()
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }

        private void Update()
        {
            if (Input.GetButton("Sprint"))
            {
                staminaBar.value -= Time.deltaTime / staminaFallRate * staminaFallMultiplier;
            }
            else
            {
                staminaBar.value += Time.deltaTime / staminaRegenRate * staminaRegenMultiplier;
            }

            if (staminaBar.value >= maxStamina)
            {
                staminaBar.value = maxStamina;
            }
            else if (staminaBar.value <= 0)
            {
                staminaBar.value = 0;

                playerMovement.sprintSpeed = playerMovement.speed;
            }
            else if (staminaBar.value >= 0)
            {
                playerMovement.sprintSpeed = playerMovement.sprintSpeedNormal;
            }
        }
    }
}