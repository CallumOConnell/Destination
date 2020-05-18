using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        public void OpenNote(NoteObject _note)
        {
            noteTitle.text = _note.title;
            noteAuthor.text = _note.author;
            noteContent.text = _note.content;
            noteThoughts.text = _note.thoughts;

            InterfaceManager.instance.OpenMenu("note");

            audioSource.clip = _note.audioClip;

            PlayAudio();
        }

        public void CloseNote()
        {
            audioSource.Stop();

            InterfaceManager.instance.CloseMenu("note");
        }

        public void PlayAudio()
        {
            if (audioSource.clip)
            {
                audioSource.Play();
            }
        }
    }
}