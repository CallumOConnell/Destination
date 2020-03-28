using UnityEngine;

namespace Destination
{
    public class DoubleClickHandler : MonoBehaviour
    {
        private float lastClick = 0f;
        private float interval = 0.4f;

        public void OnPointerClick()
        {
            if ((lastClick + interval) > Time.time) // Double click 
            {

            }
            else // Single click
            {
                lastClick = Time.time;
            }
        }
    }
}