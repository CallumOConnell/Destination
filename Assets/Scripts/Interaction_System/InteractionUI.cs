using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Destination
{
    public class InteractionUI : MonoBehaviour
    {
        public Image holdProgressImage;
        public Image tooltipBackground;

        public TextMeshProUGUI interactableTooltip;

        private RectTransform canvasTransform;

        public void Init() => GetComponents();

        private void GetComponents()
        {
            canvasTransform = GetComponent<RectTransform>();
            interactableTooltip = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetToolTip(Transform parent, string tooltip, float holdProgress)
        {
            if (parent)
            {
                canvasTransform.position = parent.position;
                canvasTransform.SetParent(parent);
            }

            interactableTooltip.SetText(tooltip);
            holdProgressImage.fillAmount = holdProgress;
        }

        public void SetTooltipActiveState(bool state)
        {
            interactableTooltip.gameObject.SetActive(state);
            holdProgressImage.gameObject.SetActive(state);
            tooltipBackground.gameObject.SetActive(state);
        }

        public void UpdateChargeProgress(float progress) => holdProgressImage.fillAmount = progress;

        public void LookAtPlayer(Transform player) => canvasTransform.LookAt(player, Vector3.up);

        public void UnparentToltip() => canvasTransform.SetParent(null);

        public bool IsTooltipActive() => interactableTooltip.gameObject.activeSelf;
    }
}