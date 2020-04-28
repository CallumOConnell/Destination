using UnityEngine;
using UnityEngine.UI;

namespace Destination
{
    public class FPS : MonoBehaviour
    {
        private Text fpsText;

        private float timer = 0;

        private void Awake() => fpsText = GetComponent<Text>();

        private void Update()
        {
            if (Time.unscaledTime > timer)
            {
                int currentFPS = (int)(1f / Time.unscaledDeltaTime);

                fpsText.text = $"FPS: {currentFPS}";

                timer = Time.unscaledTime + 1f;
            }
        }
    }
}