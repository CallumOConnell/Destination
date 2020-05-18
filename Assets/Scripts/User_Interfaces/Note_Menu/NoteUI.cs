using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

namespace Destination
{
    public class NoteUI : MonoBehaviour
    {
        [Space, Header("Audio Settings")]
        public AudioSource audioSource;

        [Space, Header("UI Settings")]
        public GameObject notePanel;

        public TextMeshProUGUI noteTitle;
        public TextMeshProUGUI noteAuthor;
        public TextMeshProUGUI noteContent;
        public TextMeshProUGUI noteThoughts;

        public Button playButton;
        public Button pauseButton;

        public void OpenNote(NoteObject _note)
        {
            noteTitle.text = _note.title;
            noteAuthor.text = _note.author;
            noteContent.text = _note.content;
            noteThoughts.text = _note.thoughts;

            InterfaceManager.instance.OpenMenu("note");

            audioSource.clip = _note.audioClip;

            audioSource.Play();

            StartCoroutine(ResetAudio(_note.audioClip.length));
        }

        public void CloseNote()
        {
            audioSource.Stop();

            InterfaceManager.instance.CloseMenu("note");
        }

        public void UnpauseAudio()
        {
            if (audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
            else
            {
                audioSource.Play();
            }

            pauseButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
        }

        public void PauseAudio()
        {
            audioSource.Pause();

            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }

        private IEnumerator ResetAudio(float _length)
        {
            yield return new WaitForSeconds(_length);

            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }
}