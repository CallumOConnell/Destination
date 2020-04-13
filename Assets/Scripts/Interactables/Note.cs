using UnityEngine;

namespace Destination
{
    public class Note : InteractableBase
    {
        public NoteObject note;

        public Canvas canvas;

        public override void OnInteract()
        {
            base.OnInteract();

            canvas.GetComponent<NoteUI>().OpenNote(note);
        }
    }
}