using UnityEngine;

namespace Destination
{
    public class SwitchWeapon : MonoBehaviour
    {
        [Space, Header("Weapon Objects")]
        public GameObject gun;
        public GameObject crowbar;

        [Space, Header("Weapon Items")]
        public ItemObject gunItem;
        public ItemObject crowbarItem;

        [Space, Header("Inventory")]
        public InventoryObject inventory;

        [HideInInspector]
        public string currentWeapon = "Gun";

        public void Switch()
        {
            if (currentWeapon == "Melee")
            {
                if (CanSwitchWeapon("Gun"))
                {
                    currentWeapon = "Gun";

                    crowbar.SetActive(false);
                    gun.SetActive(true);
                }
            }
            else
            {
                if (CanSwitchWeapon("Melee"))
                {
                    currentWeapon = "Melee";

                    gun.SetActive(false);
                    crowbar.SetActive(true);
                }
            }
        }

        private bool CanSwitchWeapon(string newWeapon)
        {
            switch (newWeapon)
            {
                case "Gun":
                {
                    return inventory.IsItemInInventory(gunItem) && currentWeapon != newWeapon;
                }

                case "Melee":
                {
                    return inventory.IsItemInInventory(crowbarItem) && currentWeapon != newWeapon;
                }
                    
                default:
                {
                    return false;
                }
            }
        }
    }
}