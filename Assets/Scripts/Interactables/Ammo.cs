using UnityEngine;

namespace Destination
{
    public class Ammo : InteractableBase
    {
        public int amount;

        public Sprite icon;

        public override void OnInteract()
        {
            base.OnInteract();

            FindObjectOfType<WeaponBase>().spareAmmo += amount;

            Destroy(gameObject);

            FindObjectOfType<Canvas>().GetComponent<PickupItem>().Display("5.56 Ammo", amount, icon);
        }
    }
}