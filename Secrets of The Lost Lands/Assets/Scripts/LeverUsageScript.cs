using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverUsageScript : MonoBehaviour
{
    private Transform player;
    public Transform lever;
    private PlayerRaycast playerRaycast;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRaycast = player.GetComponent<PlayerRaycast>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerRaycast.EnableRaycast(lever);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerRaycast.DisableRaycast(lever);
        }
    }
}
