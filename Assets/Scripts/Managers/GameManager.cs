using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Destination
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public GameObject loadingScreen;

        public ProgressBar bar;

        public TextMeshProUGUI loadingText;
        public TextMeshProUGUI tipsText;

        public CanvasGroup alphaCanvas;

        public Sprite[] backgrounds;

        public Image backgroundImage;

        public string[] tips;

        public int tipCount;

        private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        private float totalSceneProgress;
        private float totalSpawnProgress;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            instance = this;

            SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
        }

        public void LoadGame()
        {
            //backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];

            loadingScreen.SetActive(true);

            StartCoroutine(GenerateTips());

            scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
            scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAP, LoadSceneMode.Additive));

            StartCoroutine(GetSceneLoadingProgress());
            StartCoroutine(GetTotalProgress());
        }

        public void RestartGame()
        {
            //backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];

            loadingScreen.SetActive(true);

            StartCoroutine(GenerateTips());

            scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MAP));
            scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAP, LoadSceneMode.Additive));

            StartCoroutine(GetSceneLoadingProgress());
        }

        public IEnumerator GetSceneLoadingProgress()
        {
            for (int i = 0; i < scenesLoading.Count; i++)
            {
                while (!scenesLoading[i].isDone)
                {
                    totalSceneProgress = 0;

                    foreach (AsyncOperation operation in scenesLoading)
                    {
                        totalSceneProgress += operation.progress;
                    }

                    totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                    loadingText.text = $"Loading Environments: {totalSceneProgress}%";

                    yield return null;
                }
            }

            loadingScreen.SetActive(false);
        }

        public IEnumerator GetTotalProgress()
        {
            while (Initialisation.current == null || !Initialisation.current.isDone)
            {
                if (Initialisation.current == null)
                {
                    totalSpawnProgress = 0;
                }
                else
                {
                    totalSpawnProgress = Mathf.Round(Initialisation.current.progress * 100f);

                    switch (Initialisation.current.currentStage)
                    {
                        case InitialisationStage.Enemies:
                        {
                            loadingText.text = $"Spawning Enemies: {totalSpawnProgress}%";

                            break;
                        }
                        case InitialisationStage.Loot:
                        {
                            loadingText.text = $"Spawning Loot: {totalSpawnProgress}%";

                            break;
                        }
                        case InitialisationStage.Character:
                        {
                            loadingText.text = $"Creating Character: {totalSpawnProgress}%";

                            break;
                        }
                    }
                }

                float totalProgress = Mathf.Round((totalSceneProgress + totalSpawnProgress) / 2f);

                bar.current = Mathf.RoundToInt(totalProgress);

                yield return null;
            }

            loadingScreen.SetActive(false);
        }

        public IEnumerator GenerateTips()
        {
            tipCount = Random.Range(0, tips.Length);

            tipsText.text = tips[tipCount];

            while (loadingScreen.activeInHierarchy)
            {
                yield return new WaitForSeconds(3f);

                LeanTween.alphaCanvas(alphaCanvas, 0, 0.5f);

                yield return new WaitForSeconds(0.5f);

                tipCount++;

                if (tipCount >= tips.Length)
                {
                    tipCount = 0;

                    tipsText.text = tips[tipCount];

                    LeanTween.alphaCanvas(alphaCanvas, 1, 0.5f);
                }
            }
        }
    }
}