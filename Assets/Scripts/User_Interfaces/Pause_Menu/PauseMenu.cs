using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Destination
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool isPaused = false;

        public GameObject pauseMenu;

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (gamepad.startButton.wasPressedThisFrame)
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
        }

        public void Resume()
        {
            InterfaceManager.instance.CloseMenu("pause");

            Time.timeScale = 1f;

            isPaused = false;
        }

        private void Pause()
        {
            InterfaceManager.instance.OpenMenu("pause");

            Time.timeScale = 0f;

            isPaused = true;
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