using UnityEngine;

public class Initialisation : MonoBehaviour
{
    public static Initialisation current;

    public float progress;

    public bool isDone;

    public InitialisationStage currentStage;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        // Initialisation code here
        currentStage = InitialisationStage.Settings;

        // Calculate Progress
        progress = 0f;

        // Delay?

        // Script is done
        isDone = true;
    }
}

public enum InitialisationStage
{
    Enemies,
    Settings
}