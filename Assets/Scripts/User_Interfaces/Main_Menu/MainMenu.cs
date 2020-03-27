using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Destination
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu = null;
        public GameObject settingsMenu = null;

        public AudioMixer audioMixer = null;

        private void Awake() // Adjust main menu music volume
        {
            float volume = PlayerPrefs.GetFloat("VolumeSliderLevel", 0);

            audioMixer.SetFloat("volume", volume);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Level 1 - Lost");

            CloseMenu();
        }

        public void OpenSettings()
        {
            settingsMenu.SetActive(true);

            CloseMenu();
        }

        public void QuitGame() => Application.Quit();

        private void CloseMenu() => mainMenu.SetActive(false);
    }
}