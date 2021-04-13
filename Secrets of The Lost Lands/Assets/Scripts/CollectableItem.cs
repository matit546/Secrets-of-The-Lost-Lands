using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public enum ItemType { key, statuette }
    public ItemType itemType;
    public Player player;
    public GameObject UIManager;
    public int messageNumber;
    private CommunicationManager communicationManager;

    private void Start()
    {
        communicationManager = UIManager.GetComponent<CommunicationManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && itemType == ItemType.statuette)
        {
            Debug.Log("Statuette collected");
            Destroy(gameObject);
            player.collectedStatuettes += 1;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && itemType == ItemType.key)
        {
            // Click E to collect
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Key collected");
                Destroy(gameObject);
                player.collectedKeys += 1;
                communicationManager.ChangeText(messageNumber, $"{player.collectedKeys}/4");
            }

        }
    }

}
