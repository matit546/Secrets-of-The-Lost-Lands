using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    private AudioManager audioManager;
    string[] tags = { "mountains", "desert","water" };
    string currentMusic="";
    bool isMountainSoundPlaying;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (!audioManager.sounds[1].source.isPlaying)
        {
            audioManager.PlaySound("fale");
            return;
        }
        if (other.CompareTag("Player") && this.CompareTag(tags[2]))
        {
            return;
        }
      
        audioManager.StopSound("fale");
        if (other.CompareTag("Player") && this.CompareTag(tags[1]))
        {
            Debug.Log("pustynia");
            audioManager.PlaySound("pustynia");
            currentMusic = "pustynia";
        }
        else if (other.CompareTag("Player") && this.CompareTag(tags[0]))
        {
            audioManager.PlaySound("gory");
             currentMusic = "gory";
            Debug.Log("gory");

        }
        else
        {
            audioManager.PlaySound("fale");
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && this.CompareTag(tags[0]) &&!isMountainSoundPlaying)
        {
            isMountainSoundPlaying = true;
            StartCoroutine(PlayMountainSoundDelayed(15));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && this.CompareTag(tags[2])){
            return;
        }
        audioManager.StopSound(currentMusic);
        audioManager.PlaySound("fale");
        StopAllCoroutines();
    }

    IEnumerator PlayMountainSoundDelayed(int seconds)
    {
      //  seconds += seconds; //fix but please make this better ;; Edited by JD

        yield return new WaitForSeconds(seconds);

        audioManager.PlaySound("gory");
        currentMusic = "gory";
        isMountainSoundPlaying = false;
    }
}
