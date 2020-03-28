using UnityEngine;
using System.Collections;
using TMPro;

public class WeaponBase : MonoBehaviour
{
    [Space, Header("Weapon General Settings")]
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 5f;
    public float reloadTime = 1f;
    public float ammoCheckTime = 3f;

    public Camera mainCamera = null;

    public Transform impactParent = null;

    [Space, Header("Weapon Ammo Settings")]
    public int maxAmmo = 30;
    public int spareAmmo = 0;

    public GameObject ammoPanel = null;

    public TextMeshProUGUI ammoText = null;

    [Space, Header("Weapon Audio Settings")]
    public AudioClip gunShootSound = null;
    public AudioClip gunReloadSound = null;
    public AudioClip gunUnloadSound = null;
    public AudioClip fireModeSwitchSound = null;
    public AudioClip gunDryFireSound = null;

    [Space, Header("Weapon Firemode Settings")]
    public bool single = true;
    public bool burst = false;
    public bool auto = false;

    public GameObject firemodePanel = null;

    public TextMeshProUGUI firemodeText = null;

    [Space, Header("Weapon Effects")]
    public GameObject bloodEffect = null;
    public GameObject impactEffect = null;
    public GameObject hitParticles = null;

    public ParticleSystem muzzleFlash = null;

    public Animator animator;

    private AudioSource audioSource = null;

    private float nextTimeToFire = 0f;

    private int currentAmmo = 0;

    private bool isReloading = false;
    private bool isCheckingAmmo = false;
    private bool changingFireMode = false;

    private enum FireModes { Single, Burst, Auto}

    private FireModes currentFireMode = FireModes.Single;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        //currentFireMode = FireModes.Single;

        Debug.Log($"fireRate: {fireRate}");
        Debug.Log($"fireMode: {currentFireMode}");
    }

    private void Update()
    {
        if (isReloading || isCheckingAmmo || changingFireMode) return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; // .25 seconds - The greater the firerate less time between shots

            Shoot();
        }
        else
        {
            animator.SetBool("isFullAuto", false);
        }

        if (Input.GetKeyDown(KeyCode.F)) // Manual Firemode Change
        {
            StartCoroutine(ChangeFireMode());
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && spareAmmo > 0) // Manual Reload
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.V) && currentAmmo >= 1) // Manual Ammo Check
        {
            StartCoroutine(CheckAmmo());
        }

        AimDownSights();
    }

    private void Shoot()
    {
        if (currentAmmo == 0)
        {
            audioSource.PlayOneShot(gunDryFireSound);

            return;
        }

        // Muzzle flash here

        //animator.SetBool("isFullAuto", true);

        audioSource.PlayOneShot(gunShootSound);

        currentAmmo--;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hitInfo, range))
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                GameObject blood = Instantiate(bloodEffect, hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal), impactParent);

                Destroy(blood, 1f);
            }

            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            }

            GameObject bulletImpact = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), impactParent);
            GameObject hitParticleEffect = Instantiate(hitParticles, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal), impactParent);

            Destroy(bulletImpact, 2f);
            Destroy(hitParticleEffect, 1f);
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("isReloading", true);

        audioSource.PlayOneShot(gunUnloadSound); // Play weapon unload magazine sound

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("isReloading", false);

        audioSource.PlayOneShot(gunReloadSound); // Play weapon reload sound

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

        animator.SetBool("checkAmmo", true); // Start ammo check animation

        audioSource.PlayOneShot(gunUnloadSound); // Play weapon unload magazine sound

        ammoPanel.SetActive(true); // Show UI

        yield return new WaitForSeconds(ammoCheckTime);

        animator.SetBool("checkAmmo", false); // Stop ammo check animation

        audioSource.PlayOneShot(gunReloadSound); // Play weapon reload sound

        yield return new WaitForSeconds(.25f);

        ammoPanel.SetActive(false); // Hide UI

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

        firemodePanel.SetActive(true); // Show UI

        audioSource.PlayOneShot(fireModeSwitchSound);

        yield return new WaitForSeconds(2f);

        firemodePanel.SetActive(false); // Hide UI
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
                        if (single)
                        {
                            fireRate = 5f;

                            firemodeText.text = "Single";

                            //animator.SetBool("", true);

                            return;
                        }

                        break;
                    }
                case FireModes.Burst:
                    {
                        if (burst)
                        {
                            fireRate = 10;

                            firemodeText.text = "Burst";

                            return;
                        }

                        break;
                    }
                case FireModes.Auto:
                    {
                        if (auto)
                        {
                            fireRate = 15f;

                            firemodeText.text = "Auto";

                            //animator.SetBool("", true);

                            return;
                        }
                        
                        break;
                    }
            }
        }
    }

    private void AimDownSights()
    {
        if (Input.GetButton("Fire2"))
        {
            animator.SetBool("isAimDownSight", true);
        }
        else
        {
            animator.SetBool("isAimDownSight", false);
        }
    }
}