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

        public ProgressBar progressBar;

        [HideInInspector]
        public bool timerActive = false;

        private float curTime;

        private float waitTime;

        public void CreateTimer(float _waitTime)
        {
            timerActive = true;

            waitTime = _waitTime;

            curTime = waitTime;

            progressBar.gameObject.SetActive(true);

            StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            while (curTime > 0)
            {
                curTime -= Time.deltaTime;

                progressBar.current = Mathf.RoundToInt((curTime / waitTime) * progressBar.maximum);

                percentageText.text = $"{((curTime / waitTime) * 100):0}%";

                yield return null;
            }

            statusText.text = "Healed";

            yield return new WaitForSeconds(0.5f);

            timerActive = false;

            progressBar.gameObject.SetActive(false);

            statusText.text = "Healing";
        }
    }
}