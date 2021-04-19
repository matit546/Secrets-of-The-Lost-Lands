using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObjectScript : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Transform player;
    private Transform parent;
    private PlayerRaycast playerRaycast;

    private void Start()
    {
        parent = this.transform.parent;
        rigidbody = parent.GetComponent<Rigidbody>();
        playerRaycast = player.GetComponent<PlayerRaycast>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerRaycast.EnableRaycast(parent);
        }
    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Co wychodzisz");
            playerRaycast.DisableRaycast(parent);
        }
    }
}
