using UnityEngine;
using UnityEngine.UI;

namespace Destination
{
    public class PlayerVitals : MonoBehaviour
    {
        [Space, Header("Health Settings")]
        public int currentHealth;
        public int maxHealth = 100;

        public Image damageOverlay;

        [Space, Header("Inventory Settings")]
        public InventoryObject inventory;

        [Space, Header("Respawn Settings")]
        public GameObject respawnPanel;

        public InputHandler inputManager;

        private void Start() => currentHealth = maxHealth;

        public void ChangeHealth(int _amount, bool _heal)
        {
            if  (_heal)
            {
                if (currentHealth + _amount > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                else
                {
                    currentHealth += _amount;
                }
            }
            else
            {
                if (currentHealth - _amount <= 0)
                {
                    Die();
                }
                else
                {
                    currentHealth -= _amount;
                }
            }

            UpdateOverlay();
        }

        private void UpdateOverlay()
        {
           if (currentHealth <= 85)
           {
                AdjustOpacity(20f);
           }
           else if (currentHealth <= 65)
           {
                AdjustOpacity(60f);
           }
           else if (currentHealth <= 45)
           {
                AdjustOpacity(120f);
           }
           else if (currentHealth <= 20)
           {
                AdjustOpacity(200f);
           }
           else if (currentHealth <= 10)
           {
                AdjustOpacity(255f);
           }
           else if (currentHealth == 0)
           {
                AdjustOpacity(0);
           }
        }

        private void AdjustOpacity(float _alpha) => damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, _alpha);

        private void Update()
        {
            // Debug
            if (Input.GetKeyDown(KeyCode.K)) ChangeHealth(100, false);
        }

        private void OnTriggerEnter(Collider other) // Temp for testing inventory system
        {
            GroundItem item = other.GetComponent<GroundItem>();

            if (item)
            {
                inventory.AddItem(new Item(item.item), 3);

                Destroy(other.gameObject);
            }
        }

        private void Die()
        {
            respawnPanel.SetActive(true);

            inputManager.UnlockControls();
        }

        public void Respawn()
        {
            respawnPanel.SetActive(false);

            GameManager.instance.RestartGame();
        }
    }
}