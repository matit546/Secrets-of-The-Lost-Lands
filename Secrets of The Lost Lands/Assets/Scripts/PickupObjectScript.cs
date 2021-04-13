using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObjectScript : MonoBehaviour
{
    public GameObject objectHolder;
    private Rigidbody rigidbody;
    public GameObject UIManager;
    public int messageNumber;
    private CommunicationManager communicationManager;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        communicationManager = UIManager.GetComponent<CommunicationManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            communicationManager.EnableMessage(messageNumber);
            communicationManager.ChangeText(messageNumber, "Podnieœ[e]");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                communicationManager.ChangeText(messageNumber, "upuœæ [f]");
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                this.transform.position = objectHolder.transform.position;
                this.transform.rotation = objectHolder.transform.rotation;
                this.transform.SetParent(objectHolder.transform);
            }

            if (Input.GetKey(KeyCode.F))
            {
                communicationManager.ChangeText(messageNumber, "Podnieœ[e]");
                rigidbody.constraints = RigidbodyConstraints.None;
                this.transform.SetParent(null);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Co wychodzisz");
            communicationManager.DisableMessage(messageNumber);
        }
    }
}
