﻿using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Destination
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject settingsMenu;
        
        //public GameManager gameManager;

        public AudioMixer audioMixer;

        private void Awake() // Adjust main menu music volume when changing scene
        {
            float musicSliderValue = PlayerPrefs.GetFloat("MusicVolumeSliderPosition", 1);

            audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSliderValue) * 20);
        }

        public void LoadGame()
        {
            GameManager.instance.LoadGame();
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