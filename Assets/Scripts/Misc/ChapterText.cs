using System.Collections;
using UnityEngine;

namespace Destination
{
    public class ChapterText : MonoBehaviour
    {
        public GameObject text = null;

        public Animator animator = null;

        public AudioSource[] voiceLine = null;

        private BoxCollider boxCollider = null;

        private bool alreadyPlayed = false;

        private void Start() => boxCollider = GetComponent<BoxCollider>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(DisplayText());
            }
        }

        private IEnumerator DisplayText()
        {
            if (!alreadyPlayed)
            {
                voiceLine[0].Play();

                alreadyPlayed = true;
            }

            text.SetActive(true);

            yield return new WaitForSeconds(5f);

            animator.SetTrigger("fadeOut");

            yield return new WaitForSeconds(2f);

            text.SetActive(false);

            boxCollider.enabled = false;
        }
    }
}