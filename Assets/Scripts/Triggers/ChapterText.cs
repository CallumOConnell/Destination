using System.Collections;
using UnityEngine;

namespace Destination
{
    public class ChapterText : MonoBehaviour
    {
        public GameObject text;

        public Animator animator;

        public AudioSource audioSource;

        public AudioClip voiceLine;

        private bool alreadyPlayed = false;

        private BoxCollider boxCollider;

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
                audioSource.PlayOneShot(voiceLine);

                alreadyPlayed = true;

                text.SetActive(true);

                yield return new WaitForSeconds(5f);

                animator.SetTrigger("fadeOut");

                yield return new WaitForSeconds(2f);

                text.SetActive(false);

                boxCollider.enabled = false;
            }
        }
    }
}