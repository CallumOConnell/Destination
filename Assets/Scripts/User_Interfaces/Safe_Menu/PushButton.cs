using System;
using UnityEngine;
using UnityEngine.UI;

public class PushButton : MonoBehaviour
{
    public static event Action<string> ButtonPressed = delegate { };

    private int deviderPosition;

    private string buttonName, buttonValue;

    private void Start()
    {
        buttonName = gameObject.name;
        deviderPosition = buttonName.IndexOf("_"); // Finds index of underscore in game object name
        buttonValue = buttonName.Substring(0, deviderPosition); // Stores the first half of game object name

        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked() => ButtonPressed(buttonValue);
}
