using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject _endMenu;
    [SerializeField] private GameObject _hud;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            if (_hud != null)
            {
                _hud.SetActive(false);
            }

            if (_endMenu != null)
            {
                _endMenu.SetActive(true);
            }

            if (_audioSource != null && _audioClip != null)
            {
                _audioSource.PlayOneShot(_audioClip);
            }
        }
    }
}
