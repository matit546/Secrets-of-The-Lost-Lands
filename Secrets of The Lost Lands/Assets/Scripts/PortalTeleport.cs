using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform teleportLocation;
    CharacterController cc;
    private void OnTriggerEnter(Collider other)
    {
        cc = other.GetComponent<CharacterController>();
        if (other.tag =="Player")
        {
            cc.enabled = false;

            AudioManager.musicManagerInstance.StopAllSound();
            other.transform.position = teleportLocation.position;
            AudioManager.musicManagerInstance.PlaySound("portal");
            cc.enabled = true;


        }

        
    }
}
