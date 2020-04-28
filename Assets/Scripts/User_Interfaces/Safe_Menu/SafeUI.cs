using System.Collections;
using TMPro;
using UnityEngine;

namespace Destination
{
    public class SafeUI : MonoBehaviour
    {
        [Space, Header("UI Settings")]
        public TMP_InputField inputText;

        [Space, Header("Safe Settings")]
        public string correctCode = "1234";

        public GameObject safeObject;
        public GameObject safeMenu;

        [Space, Header("Manager Settings")]
        public InputHandler inputHandler;

        private Safe safe;

        private AudioSource audioSource;

        private string code;

        private void Start()
        {
            audioSource = safeMenu.GetComponent<AudioSource>();
            safe = safeObject.GetComponent<Safe>();

            ResetInput();

            ButtonHandler.ButtonPressed += AddDigitToCodeSequence;
        }

        private void Update()
        {
            if (safeMenu.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CloseMenu();
                }
            }
        }

        private void AddDigitToCodeSequence(string digitEntered)
        {
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
                }
            }

            if (digitEntered == "Clear")
            {
                ResetInput();
            }
            
            if (code.Length == 4)
            {
                StartCoroutine(CheckCode());
            }
        }

        private void DisplayCodeSequence() => inputText.text = code;

        private void ResetInput()
        {
            code = "";
            inputText.text = "";
        }

        private IEnumerator CheckCode()
        {
            yield return new WaitForSeconds(0.5f);

            if (code == correctCode)
            {
                CloseMenu();

                safe.OpenSafe();

                safe.gameObject.layer = 0;
            }
            else
            {
                audioSource.Play();

                ResetInput();
            }
        }

        private void CloseMenu()
        {
            safeMenu.SetActive(false);

            inputHandler.LockControls();
        }

        private void OnDestroy() => ButtonHandler.ButtonPressed -= AddDigitToCodeSequence; // Removes event handler when button no longer exists avoids null ref errors
    }
}