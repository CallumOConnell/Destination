using TMPro;

namespace Destination
{
    public class Ammo : InteractableBase
    {
        public WeaponBase weaponBase;

        public int amount;

        public TextMeshProUGUI pickupText;

        public override void OnInteract()
        {
            base.OnInteract();

            weaponBase.spareAmmo += amount;

            pickupText.text = $"+ 5.56 x {amount}";
        }
    }
}