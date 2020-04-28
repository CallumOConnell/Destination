using UnityEngine;

namespace Destination
{
    public class Ammo : InteractableBase
    {
        public WeaponBase weaponBase;

        public int amount;

        public PickupItem pickupItem;

        public Sprite icon;

        public override void OnInteract()
        {
            base.OnInteract();

            weaponBase.spareAmmo += amount;

            Destroy(gameObject);

            pickupItem.Display("5.56 Ammo", amount, icon);
        }
    }
}