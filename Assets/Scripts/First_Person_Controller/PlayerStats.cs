using UnityEngine;

namespace Destination
{
    public class PlayerStats : MonoBehaviour
    {
        [Space, Header("Health Settings")]
        public int currentHealth;
        public int maxHealth = 100;

        [Space, Header("Inventory Settings")]
        public InventoryObject inventory;

        [Space, Header("Respawn Settings")]
        public Transform respawnPosition;

        public RandomItemSpawning lootManager;

        public RespawnEnemies enemyManager;

        public QuestManager questManager;

        public WeaponBase weapon;

        public GameObject respawnPanel;

        public ChapterText chapterText;

        public AudioManager audioManager;

        private void Start() => currentHealth = maxHealth;

        public void TakeDamage(int _amount)
        {
            // Create blood screen effect system depending on players current health

            currentHealth -= _amount;

            if (currentHealth <= 0) Respawn();
        }

        private void Update()
        {
            // Debug
            if (Input.GetKeyDown(KeyCode.P)) currentHealth -= 10;
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

        private void Respawn()
        {
            // Show Death Screen
            respawnPanel.SetActive(true);

            // Teleport to the beginning of the map
            transform.position = respawnPosition.position;

            // Reset inventory contents
            inventory.Clear();

            // Repopulate loot
            lootManager.PopulateItems();

            // Reset variables
            currentHealth = maxHealth; // Reset health
            weapon.currentAmmo = 0; // Reset ammo
            weapon.spareAmmo = 90;

            // Reset enemies
            enemyManager.SpawnEnemies();

            // Reset quests
            questManager.SetupQuests();

            // Reset chapter text
            chapterText.alreadyPlayed = false;

            // Reset voice trigger
            audioManager.ResetTriggers();
        }
    }
}