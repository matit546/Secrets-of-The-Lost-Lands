using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public ParticleSystem animation;
    public Image panel;
    public GameObject pauseMenuUI;
    public Text text;
    public Player mainPlayer;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Player")
        {
            animation.Play();
            other.GetComponent<CharacterController>().enabled = false;
            StartCoroutine(Rise(other.transform));
        }
    }

    public IEnumerator Rise(Transform player)
    {
        for (int i = 0; i < 200; i++)
        {
            player.Translate(0, 0.08f, 0);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a + (float)(1f / 200));
            yield return null;

        }
        pauseMenuUI.SetActive(true);
        var ts = TimeSpan.FromSeconds(mainPlayer.gameTime);
        text.text = "Czas gry:  "+string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
        AudioManager.musicManagerInstance.StopAllSound();
    }
}
