using UnityEngine;
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

        private PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();

            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }

        private void Update()
        {
            bool isMoving = Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0;

            if (isMoving && Input.GetButton("Sprint"))
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