using TMPro;
using UnityEngine;

namespace Destination
{
    public class SafeUI : MonoBehaviour
    {
        public TMP_InputField inputText = null;

        public GameObject safeObject = null;
        public GameObject safeCodeUI = null;

        private Safe safe = null;

        private string code;

        private void Start()
        {
            safe = safeObject.GetComponent<Safe>();

            ResetInput();

            PushButton.ButtonPressed += AddDigitToCodeSequence;
        }

        private void Update()
        {
            if (safeCodeUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    safeCodeUI.SetActive(false);
                }
            }
        }

        private void AddDigitToCodeSequence(string digitEntered)
        {
            Debug.Log("A");

            if (code.Length < 4)
            {
                switch (digitEntered)
                {
                    case "Zero":
                        {
                            code += "0";
                            DisplayCodeSequence();
                            break;
                        }
                    case "One":
                        {
                            code += "1";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Two":
                        {
                            code += "2";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Three":
                        {
                            code += "3";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Four":
                        {
                            code += "4";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Five":
                        {
                            code += "5";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Six":
                        {
                            code += "6";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Seven":
                        {
                            code += "7";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Eight":
                        {
                            code += "8";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Nine":
                        {
                            code += "9";
                            DisplayCodeSequence();
                            break;
                        }
                    case "Clear":
                        {
                            ResetInput();
                            break;
                        }
                }
            }
            else if (code.Length == 4)
            {
                CheckCode();
            }
        }

        private void DisplayCodeSequence() => inputText.text = code;

        private void ResetInput()
        {
            code = "";
            inputText.text = "";
        }

        private void CheckCode()
        {
            if (code == "1234")
            {
                Debug.Log("Code Entered Correctly");
                safeCodeUI.SetActive(false);
                safe.OpenSafe();
            }
            else
            {
                Debug.Log("Code Entered Incorrect");
                // Play Error Sound
                ResetInput();
            }
        }

        private void OnDestroy() => PushButton.ButtonPressed -= AddDigitToCodeSequence; // Removes event handler when button no longer exists avoids null ref errors
    }
}