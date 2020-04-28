using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Destination
{
    public class PickupItem : MonoBehaviour
    {
        public GameObject itemPickupPanel;

        public TextMeshProUGUI itemName;

        public TextMeshProUGUI itemAmount;

        public Image itemIcon;

        public void Display(string _name, int _amount, Sprite _icon)
        {
            StartCoroutine(DisplayUI(_name, _amount, _icon));
        }
        
        public IEnumerator DisplayUI(string _name, int _amount, Sprite _icon)
        {
            itemName.text = _name;

            itemAmount.text = $"+ {_amount}";

            itemIcon.sprite = _icon;

            itemPickupPanel.SetActive(true);

            yield return new WaitForSeconds(2f);

            itemPickupPanel.SetActive(false);
        }
    }
}