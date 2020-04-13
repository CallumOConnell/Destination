using UnityEngine;

namespace Destination
{
    public class PlayerStats : MonoBehaviour
    {
        [Space, Header("Player Health")]
        public int maxHealth = 100;

        public InventoryObject inventory;

        public int currentHealth;

        private void Start() => currentHealth = maxHealth;

        private void Update()
        {
            // Debug
            if (Input.GetKeyDown(KeyCode.P)) currentHealth -= 10;

            /*
                Might be worth adding this to a function where the player takes damage then check if they are dead after deducting the damage.

                Don't quote me on this but changing active status on a game object the script is attached to can cause problems I think I had
                trouble with it on my college game but idk till further testing.


            */
            if (currentHealth <= 0) gameObject.SetActive(false);
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
    }
}