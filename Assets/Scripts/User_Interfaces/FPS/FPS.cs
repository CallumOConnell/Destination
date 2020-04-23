using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{  
    private float hudRefreshRate = 1f;

    private Text fpsText = null;

    private float timer = 0;

    private void Awake() => fpsText = GetComponent<Text>();

    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int currentFPS = (int)(1f / Time.unscaledDeltaTime);

            fpsText.text = $"FPS: {currentFPS}";

            timer = Time.unscaledTime + hudRefreshRate;
        }
    }
}