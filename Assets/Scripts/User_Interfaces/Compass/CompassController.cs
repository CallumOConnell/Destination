using UnityEngine;
using UnityEngine.UI;

namespace Destination
{
    public class CompassController : MonoBehaviour
    {
        public GameObject compassPointer;
        public GameObject compassTarget;
        public GameObject player;

        public RawImage compassLine;

        private RectTransform rect;

        private void Start() => rect = compassPointer.GetComponent<RectTransform>();

        private void Update()
        {
            compassLine.uvRect = new Rect(player.transform.localEulerAngles.y / 360, 0, 1, 1);

            Vector3[] v = new Vector3[4];

            compassLine.rectTransform.GetLocalCorners(v);

            float pointerScale = Vector3.Distance(v[1], v[2]); // Both bottom corners

            Vector3 direction = compassTarget.transform.position - player.transform.position;

            float angleToTarget = Vector3.SignedAngle(player.transform.forward, direction, player.transform.up);

            angleToTarget = Mathf.Clamp(angleToTarget, -90, 90) / 180.0f * pointerScale;

            rect.localPosition = new Vector3(angleToTarget, rect.localPosition.y, rect.localPosition.z);
        }
    }
}