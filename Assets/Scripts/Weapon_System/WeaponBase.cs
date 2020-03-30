using UnityEngine;
using System.Collections;
using TMPro;

public class WeaponBase : MonoBehaviour
{
    [Space, Header("Weapon General Settings")]
    public float damage = 1f;
    public float range = 1f;
    public float impactForce = 1f;
    public float fireRate = 5f;
    public float reloadWaitTime = 1f;
    public float ammoCheckWaitTime = 3f;
    public float firemodeWaitTime = 2f;

    public Camera mainCamera;

    public Transform impactParent;

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

    [Space, Header("Weapon Firemode Settings")]
    public bool single = true;
    public bool burst = false;
    public bool auto = false;

    public GameObject firemodePanel;

    public TextMeshProUGUI firemodeText;

    [Space, Header("Weapon ADS")]
    public Vector3 aimPos;
    public float adsSpeed = 8;
    private Vector3 originalPos;
    private bool isAiming;

    [Space, Header("Weapon Effects")]
    public GameObject bloodEffect;
    public GameObject impactEffect;
    public GameObject hitParticles;

    public ParticleSystem muzzleFlash;

    private Animator animator;
    private AnimatorClipInfo info;

    private AudioSource audioSource;

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
        animator = GetComponent<Animator>();

        currentAmmo = maxAmmo;

        originalPos = transform.localPosition;

    }

    private void Update()
    {
        if (isReloading || isCheckingAmmo || changingFireMode) return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; // .25 seconds - The greater the firerate less time between shots
            Shoot();
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

    private void FixedUpdate()
    {

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("HK416_Fire")) animator.SetBool("isFire", false);

        animator.SetBool("isAiming", isAiming);

    }

    private void Shoot()
    {
        if (currentAmmo == 0)
        {
            audioSource.PlayOneShot(gunDryFireSound);

            return;
        }

        // Muzzle flash here

        audioSource.PlayOneShot(gunShootSound);

        animator.CrossFadeInFixedTime("HK416_Fire", 0.1f);

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

        //animator.SetBool("isReloading", true);

        audioSource.PlayOneShot(gunUnloadSound); // Play weapon unload magazine sound

        yield return new WaitForSeconds(reloadWaitTime - .25f);

        //animator.SetBool("isReloading", false);

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

        //animator.SetBool("checkAmmo", true);

        audioSource.PlayOneShot(gunUnloadSound);

        ammoPanel.SetActive(true);

        yield return new WaitForSeconds(ammoCheckWaitTime);

        //animator.SetBool("checkAmmo", false);

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
                        if (single)
                        {
                            fireRate = 5f;

                            firemodeText.text = "Single";

                            

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

                            return;
                        }
                        
                        break;
                    }
            }
        }
    }

    private void AimDownSights()
    {
        if (Input.GetButton("Fire2") && !isReloading)
        {

            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * adsSpeed);
            isAiming = true;

        }
        else
        {

            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * adsSpeed);
            isAiming = false;

        }
    }
}