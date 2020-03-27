using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Destination
{
    public class SettingsMenu : MonoBehaviour
    {
        public Dropdown resolutionDropDown;

        public Dropdown graphicsDropDown;

        public Slider volumeSlider;

        public AudioMixer masterMixer;

        private Resolution[] resolutions;

        private void Start()
        {
            resolutions = Screen.resolutions;

            resolutionDropDown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;

                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropDown.AddOptions(options);

            resolutionDropDown.value = currentResolutionIndex;

            resolutionDropDown.RefreshShownValue();

            float sliderPosition = PlayerPrefs.GetFloat("MasterVolume", 1);

            volumeSlider.value = sliderPosition;

            int currentGraphicsQuality = QualitySettings.GetQualityLevel();

            graphicsDropDown.value = currentGraphicsQuality;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

        public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;

        public void SetMasterVolume(float sliderValue)
        {
            PlayerPrefs.SetFloat("MasterVolumeSliderPosition", sliderValue);

            float volume = Mathf.Log10(sliderValue) * 20;

            masterMixer.SetFloat("MasterVolume", volume);

            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public void SetSfxVolume(float sliderValue)
        {
            PlayerPrefs.SetFloat("SfxVolumeSliderPosition", sliderValue);

            float volume = Mathf.Log10(sliderValue) * 20;

            masterMixer.SetFloat("SfxVolume", volume);

            PlayerPrefs.SetFloat("SfxVolume", volume);
        }

        public void SetMusicVolume(float sliderValue)
        {
            PlayerPrefs.SetFloat("MusicVolumeSliderPosition", sliderValue);

            float volume = Mathf.Log10(sliderValue) * 20;

            masterMixer.SetFloat("MusicVolume", volume);

            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }
}