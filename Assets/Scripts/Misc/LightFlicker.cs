using UnityEngine;
using System.Collections.Generic;

public class LightFlicker : MonoBehaviour
{
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 5;

    private Light _light;

    private Queue<float> smoothQueue;
    
    private float lastSum = 0;

    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    private void Start()
    {
        smoothQueue = new Queue<float>(smoothing);

        _light = GetComponent<Light>();
    }

    private void Update()
    {
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(minIntensity, maxIntensity);

        smoothQueue.Enqueue(newVal);

        lastSum += newVal;

        _light.intensity = lastSum / smoothQueue.Count;
    }
}