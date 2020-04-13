using UnityEngine;

namespace Destination
{
    public class BulletHole : MonoBehaviour
    {
        public float despawnTime;

        private void Start() => Destroy(gameObject, despawnTime);
    }
}