using System.Collections;
using TMPro;
using UnityEngine;

namespace Destination
{
    public class SafeUI : MonoBehaviour
    {
        public TMP_InputField inputText;

        public GameObject safeObject;
        public GameObject safeCodeUI;

        public AudioSource audioSource;

        public AudioClip errorSound;

        public string correctCode = "1234";

        private Safe safe;

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
            yield return new WaitForSeconds(1f);

            if (code == correctCode)
            {
                safeCodeUI.SetActive(false);
                safe.OpenSafe();
            }
            else
            {
                audioSource.PlayOneShot(errorSound);
                ResetInput();
            }
        }

        private void OnDestroy() => PushButton.ButtonPressed -= AddDigitToCodeSequence; // Removes event handler when button no longer exists avoids null ref errors
    }
}