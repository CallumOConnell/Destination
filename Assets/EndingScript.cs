using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{

    public GameObject EndingUI;
    public GameObject HUD;
    public AudioSource AS;
    public AudioClip endingSong;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            HUD.SetActive(false);
            EndingUI.SetActive(true);
            AS.PlayOneShot(endingSong);    

        }

    }
}
