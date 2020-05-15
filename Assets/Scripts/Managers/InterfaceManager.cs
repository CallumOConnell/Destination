using UnityEngine;

namespace Destination
{
    public class InterfaceManager : MonoBehaviour
    {
        public static InterfaceManager instance;

        public Canvas canvas;

        public GameObject questMenu;
        public GameObject inventoryMenu;
        public GameObject safeMenu;
        public GameObject pauseMenu;
        public GameObject noteMenu;
        public GameObject respawnMenu;
        public GameObject hudMenu;

        private string currentMenu = "";

        public bool inDialog = false;

        private void Awake() => instance = this;

        public void OpenMenu(string _menuName)
        {
            if (currentMenu == "")
            {
                hudMenu.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                switch (_menuName)
                {
                    case "quest":
                    {
                        currentMenu = "quest";

                        questMenu.SetActive(true);

                        canvas.GetComponent<QuestUI>().UpdateUI();

                        break;
                    }
                    case "inventory":
                    {
                        currentMenu = "inventory";

                        inventoryMenu.SetActive(true);

                        break;
                    }
                    case "safe":
                    {
                        currentMenu = "safe";

                        safeMenu.SetActive(true);

                        break;
                    }
                    case "pause":
                    {
                        currentMenu = "pause";

                        pauseMenu.SetActive(true);

                        break;
                    }
                    case "note":
                    {
                        currentMenu = "note";

                        noteMenu.SetActive(true);

                        break;
                    }
                    case "respawn":
                    {
                        currentMenu = "respawn";

                        respawnMenu.SetActive(true);

                        break;
                    }
                }

                inDialog = true;
            }
            else
            {
                if (currentMenu == _menuName)
                {
                    CloseMenu(_menuName);
                }
                else
                {
                    return;
                }
            }
        }

        public void CloseMenu(string _menuName)
        {
            switch (_menuName)
            {
                case "quest":
                {
                    questMenu.SetActive(false);

                    break;
                }
                case "inventory":
                {
                    inventoryMenu.SetActive(false);

                    break;
                }
                case "safe":
                {
                    safeMenu.SetActive(false);

                    break;
                }
                case "pause":
                {
                    pauseMenu.SetActive(false);

                    break;
                }
                case "note":
                {
                    noteMenu.SetActive(false);

                    break;
                }
                case "respawn":
                {
                    respawnMenu.SetActive(false);

                    break;
                }
            }

            hudMenu.SetActive(true);
            inDialog = false;
            currentMenu = "";
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}