using UnityEngine;

namespace Destination
{
    public class Note : InteractableBase
    {
        [Space, Header("Note Settings")]

        public NoteObject note;

        [Space, Header("UI Settings")]

        public Canvas canvas;

        public override void OnInteract()
        {
            base.OnInteract();

            canvas.GetComponent<NoteUI>().OpenNote(note);
        }
    }
}