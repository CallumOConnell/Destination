using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Destination
{
    public class InteractionUI : MonoBehaviour
    {
        public Image progressBar;
        public Image fill;

        public TextMeshProUGUI text;

        public void SetToolTip(string tooltip) => text.SetText(tooltip);

        public void SetTooltipActiveState(bool state) => progressBar.gameObject.SetActive(state);

        public void UpdateChargeProgress(float progress) => fill.fillAmount = progress;
    }
}