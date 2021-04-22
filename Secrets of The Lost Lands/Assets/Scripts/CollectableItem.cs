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
    public Sprite sprite;
    public int pickObjectCommunicate = 0;
    public int  keyCollectedText=1;

    private void Start()
    {
        communicationManager = UIManager.GetComponent<CommunicationManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && itemType == ItemType.statuette)
        {
            communicationManager.EnableMessage(messageNumber);
            communicationManager.ChangeText(messageNumber, "Zebra³eœ statuetkê");
            player.collectedStatuettes += 1;
            communicationManager.DisableMessageCourotine(messageNumber, 3);
            Destroy(gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && itemType == ItemType.key)
        {
            communicationManager.EnableMessage(pickObjectCommunicate);
            communicationManager.ChangeText(pickObjectCommunicate, "Podnieœ Klucz");
            communicationManager.ChangeSprite(pickObjectCommunicate, sprite);
            if (Input.GetKey(KeyCode.E))
            {
                communicationManager.DisableMessage(pickObjectCommunicate);
                communicationManager.EnableMessage(messageNumber);
                communicationManager.ChangeText(messageNumber, "Zebra³eœ Klucz");
                player.collectedKeys += 1;
                communicationManager.ChangeText(keyCollectedText, $"{player.collectedKeys}/{player.maxCollectedKeys}");
                communicationManager.DisableMessageCourotine(messageNumber,3);
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        communicationManager.DisableMessage(pickObjectCommunicate);
    }




}
