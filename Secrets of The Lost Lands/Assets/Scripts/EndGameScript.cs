using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public ParticleSystem animation;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
            yield return null;
        }
    }
}
