using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

namespace Destination
{
    public class WeaponBase : MonoBehaviour
    {
        [Space, Header("Weapon General Settings")]
        public float damage = 1f;
        public float range = 1f;
        public float impactForce = 1f;
        public float fireRate = 0.2f;
        public float reloadWaitTime = 1f;
        public float ammoCheckWaitTime = 3f;
        public float firemodeWaitTime = 2f;

        public Camera mainCamera;

        [Space, Header("Weapon Ammo Settings")]
        public int maxAmmo = 30;
        public int spareAmmo = 90;

        public GameObject ammoPanel;

        public TextMeshProUGUI ammoText;

        [Space, Header("Weapon Audio Settings")]
        public AudioClip gunShootSound;
        public AudioClip gunReloadSound;
        public AudioClip gunUnloadSound;
        public AudioClip fireModeSwitchSound;
        public AudioClip gunDryFireSound;
        public AudioClip bulletDropSound;

        [Space, Header("Weapon Firemode Settings")]
        public bool isSingle = true;
        public bool isBurst = false;
        public bool isAutomatic = false;

        public int burstAmount = 3;

        public GameObject firemodePanel;

        public TextMeshProUGUI firemodeText;

        [Space, Header("Weapon ADS")]
        public Vector3 aimPos;

        public float adsSpeed = 8;

        [Space, Header("Weapon Effects")]
        public GameObject bloodEffect;
        public GameObject hitParticles;
        public GameObject bulletHole;

        public Transform tempEffects;

        public ParticleSystem muzzleFlash;

        private Animator animator;

        private AudioSource audioSource;

        private Vector3 originalPos;

        private float nextTimeToFire = 0f;

        public int currentAmmo = 0;

        private bool isAiming = false;
        private bool isReloading = false;
        private bool isCheckingAmmo = false;
        private bool changingFireMode = false;

        private enum FireModes { Single, Burst, Auto }

        private FireModes currentFireMode = FireModes.Single;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();

            currentAmmo = maxAmmo;

            originalPos = transform.localPosition;
        }

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (isReloading || isCheckingAmmo || changingFireMode || InterfaceManager.instance.inDialog) return;

                if (Time.time >= nextTimeToFire && Time.timeScale > 0)
                {
                    if (isAutomatic && currentFireMode == FireModes.Auto)
                    {
                        if (gamepad.rightShoulder.isPressed)
                        {
                            Shoot();
                        }
                    }
                    else if (isBurst && currentFireMode == FireModes.Burst)
                    {
                        if (gamepad.rightShoulder.wasPressedThisFrame)
                        {
                            StartCoroutine(BurstFire());
                        }
                    }
                    else if (isSingle && currentFireMode == FireModes.Single)
                    {
                        if (gamepad.rightShoulder.wasPressedThisFrame)
                        {
                            Shoot();
                        }
                    }
                }

                if (gamepad.leftTrigger.wasPressedThisFrame) // Manual Firemode Change
                {
                    StartCoroutine(ChangeFireMode());
                }

                if (gamepad.rightTrigger.wasPressedThisFrame && currentAmmo < maxAmmo && spareAmmo > 0) // Manual Reload
                {
                    StartCoroutine(Reload());
                }

                if (gamepad.dpad.left.wasPressedThisFrame && currentAmmo >= 1) // Manual Ammo Check
                {
                    StartCoroutine(CheckAmmo());
                }

                AimDownSights();
            }
        }

        private void FixedUpdate()
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

            if (info.IsName("HK416_Fire")) animator.SetBool("isFire", false);

            animator.SetBool("isAiming", isAiming);
        }

        private IEnumerator DropCasing()
        {
            yield return new WaitForSeconds(0.5f);

            audioSource.PlayOneShot(bulletDropSound);
        }

        private void Shoot()
        {
            nextTimeToFire = Time.time + fireRate;

            if (currentAmmo == 0)
            {
                audioSource.PlayOneShot(gunDryFireSound);

                return;
            }

            // Muzzle flash here

            audioSource.PlayOneShot(gunShootSound);

            StartCoroutine(DropCasing());

            animator.CrossFadeInFixedTime("HK416_Fire", 0.1f);

            currentAmmo--;

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hitInfo, range))
            {
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);

                    GameObject blood = Instantiate(bloodEffect, hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), tempEffects);

                    Destroy(blood, 1f);
                }

                if (hitInfo.rigidbody != null)
                {
                    hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
                }

                if (hitInfo.collider.CompareTag("Ground"))
                {
                    Instantiate(bulletHole, hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), tempEffects);
                }

                GameObject hitParticleEffect = Instantiate(hitParticles, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal), tempEffects);

                Destroy(hitParticleEffect, 1f);
            }
        }

        private IEnumerator BurstFire()
        {
            for (int i = 0; i < burstAmount; i++)
            {
                Shoot();

                yield return new WaitForSeconds(fireRate);
            }
        }

        private IEnumerator Reload()
        {
            isReloading = true;

            audioSource.PlayOneShot(gunUnloadSound);

            yield return new WaitForSeconds(reloadWaitTime - .25f);

            audioSource.PlayOneShot(gunReloadSound);

            yield return new WaitForSeconds(.25f);

            int requiredAmmo = maxAmmo - currentAmmo;

            if (spareAmmo >= requiredAmmo) // Has more spare ammo than required to fill magazine. E.G (90 - 15)
            {
                currentAmmo += requiredAmmo;
                spareAmmo -= requiredAmmo;
            }
            else // Has less spare ammo that's required to fill the magazine. E.G (5 - 10)
            {
                currentAmmo += spareAmmo;
                spareAmmo -= spareAmmo;
            }

            isReloading = false;
        }

        private IEnumerator CheckAmmo()
        {
            isCheckingAmmo = true;

            DisplayAmmoCount();

            audioSource.PlayOneShot(gunUnloadSound);

            ammoPanel.SetActive(true);

            yield return new WaitForSeconds(ammoCheckWaitTime);

            audioSource.PlayOneShot(gunReloadSound);

            yield return new WaitForSeconds(.25f);

            ammoPanel.SetActive(false);

            isCheckingAmmo = false;
        }

        private void DisplayAmmoCount()
        {
            float newMaxAmmo = float.Parse(maxAmmo.ToString());
            float newCurrentAmmo = float.Parse(currentAmmo.ToString());

            float ammoPercentage = Mathf.Round((newCurrentAmmo / newMaxAmmo) * 100);

            if (ammoPercentage == 100) // 100%
            {
                ammoText.SetText("Full");
            }

            if (ammoPercentage >= 51 && ammoPercentage <= 99) // 51 - 99%
            {
                ammoText.SetText("More Than Half");
            }

            if (ammoPercentage == 50) // 50%
            {
                ammoText.SetText("Half Full");
            }

            if (ammoPercentage <= 49 && ammoPercentage >= 16) // 16 - 49%
            {
                ammoText.SetText("Less Than Half");
            }

            if (ammoPercentage <= 15 && ammoPercentage >= 1) // 1 - 15%
            {
                ammoText.SetText("Almost Empty");
            }
        }

        private IEnumerator ChangeFireMode()
        {
            changingFireMode = true;

            DisplayFireMode();

            changingFireMode = false;

            firemodePanel.SetActive(true);

            audioSource.PlayOneShot(fireModeSwitchSound);

            yield return new WaitForSeconds(firemodeWaitTime);

            firemodePanel.SetActive(false);
        }

        private void DisplayFireMode()
        {
            for (int i = 0; i < 3; i++)
            {
                int mode = (int)currentFireMode + 1; // Select next fire mode

                if (mode > 2) // Wrap around after last mode
                {
                    mode = 0;
                }

                currentFireMode = (FireModes)mode;

                switch (currentFireMode)
                {
                    case FireModes.Single:
                    {
                        if (isSingle)
                        {
                            fireRate = 0.3f;

                            firemodeText.text = "Single";

                            return;
                        }

                        break;
                    }
                    case FireModes.Burst:
                    {
                        if (isBurst)
                        {
                            fireRate = 0.2f;

                            firemodeText.text = "Burst";

                            return;
                        }

                        break;
                    }
                    case FireModes.Auto:
                    {
                        if (isAutomatic)
                        {
                            fireRate = 0.2f;

                            firemodeText.text = "Auto";

                            return;
                        }

                        break;
                    }
                }
            }
        }

        private void AimDownSights()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (gamepad.leftShoulder.isPressed)
                {
                    isAiming = true;

                    transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * adsSpeed);
                }
                else
                {
                    isAiming = false;

                    transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * adsSpeed);
                }
            }
        }
    }
}