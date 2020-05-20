using UnityEngine;

public class EndingScript : MonoBehaviour
{
    public GameObject endingMenu;
    public GameObject hud;

    public AudioSource audioSource;

    public AudioClip endingSong;

    private void OnTriggerEnter(Collider _other)
    {

        if (_other.CompareTag("Player"))
        {
            hud.SetActive(false);

            endingMenu.SetActive(true);
            
            audioSource.PlayOneShot(endingSong);    
        }
    }
}
