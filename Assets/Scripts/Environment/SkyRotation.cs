using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SkyRotation : MonoBehaviour
{
    public Volume volume;

    public float rotationSpeed = 1.2f;

    private void Update() => SetHDRIRotation(Time.time * rotationSpeed);

    private void SetHDRIRotation(float rot)
    {
        volume.profile.TryGet(out HDRISky sky);

        sky.rotation.value = rot;
    }
}
