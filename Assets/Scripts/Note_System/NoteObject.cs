using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Note System/Note")]
public class NoteObject : ScriptableObject
{
    public string title;
    public string author;
    [TextArea(15, 20)] public string content;
    [TextArea(15, 20)] public string thoughts;

    public AudioClip audioClip;
}
