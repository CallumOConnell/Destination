using UnityEngine;
using UnityEngine.SceneManagement;

namespace Destination
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool isPaused = false;

        public GameObject pauseMenu;

        public InputHandler inputHandler;

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

            inputHandler.LockControls();
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0f;

            isPaused = true;

            inputHandler.UnlockControls();
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("Main_Menu");

            Time.timeScale = 1f;
        }

        public void QuitGame() => Application.Quit();
    }
}