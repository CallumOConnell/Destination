using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Destination
{
    public class NoteUI : MonoBehaviour
    {
        [Space, Header("Audio Settings")]
        public AudioSource audioSource;

        [Space, Header("Input Settings")]
        public InputHandler inputHandler;

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
            inputHandler.UnlockControls();

            noteTitle.text = _note.title;
            noteAuthor.text = _note.author;
            noteContent.text = _note.content;
            noteThoughts.text = _note.thoughts;

            notePanel.SetActive(true);

            audioSource.clip = _note.audioClip;

            audioSource.Play();
        }

        public void CloseNote()
        {
            audioSource.Stop();

            notePanel.SetActive(false);

            inputHandler.LockControls();
        }

        public void UnpauseAudio()
        {
            audioSource.UnPause();

            pauseButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }

        public void PauseAudio()
        {
            audioSource.Pause();

            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }
}