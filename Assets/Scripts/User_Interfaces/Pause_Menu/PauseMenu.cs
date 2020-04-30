using UnityEngine;
using UnityEngine.SceneManagement;

namespace Destination
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool isPaused = false;

        public GameObject pauseMenu;

        public InputHandler inputManager;

        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1f;

            isPaused = false;

            inputManager.LockControls();
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0f;

            isPaused = true;

            inputManager.UnlockControls();
        }

        public void LoadMenu()
        {
            SceneManager.UnloadSceneAsync((int)SceneIndexes.MAP);
            SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);

            Time.timeScale = 1f;
        }

        public void QuitGame() => Application.Quit();
    }
}