using UnityEngine;
using TMPro;
using System.Collections;

namespace Destination
{
    public class Timer : MonoBehaviour
    {
        [Space, Header("UI Settings")]
        public TextMeshProUGUI percentageText;
        public TextMeshProUGUI statusText;

        public ProgressBar bar;

        [HideInInspector]
        public bool timerActive = false;

        private float curTime;

        private float waitTime;

        public void CreateTimer(float _waitTime)
        {
            timerActive = true;

            waitTime = _waitTime;

            curTime = waitTime;

            bar.gameObject.SetActive(true);

            StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            while (curTime > 0)
            {
                curTime -= Time.deltaTime;

                bar.current = Mathf.RoundToInt((curTime / waitTime) * bar.maximum);

                percentageText.text = $"{((curTime / waitTime) * 100):0}%";

                yield return null;
            }

            statusText.text = "Healed";

            yield return new WaitForSeconds(0.5f);

            timerActive = false;

            bar.gameObject.SetActive(false);
        }
    }
}