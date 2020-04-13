using UnityEngine;

namespace Destination
{
    public class Medkit : ItemObject
    {
        public override void Use()
        {
            base.Use();

            Debug.Log("B");

            //StartCoroutine(ApplyMedkit());
        }
    }
}