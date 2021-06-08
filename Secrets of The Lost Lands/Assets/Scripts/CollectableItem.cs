using System;
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
    private SaveGameManager saveGameManager;
    public DisableBarrierScript DisableBarrierScript;
    string[] positionNames = { "PositionKey1", "PositionKey2", "PositionKey3", "PositionKey4" };

    private void Start()
    {
        communicationManager = UIManager.GetComponent<CommunicationManager>();
        saveGameManager = GameObject.Find("SaveGameManager").GetComponent<SaveGameManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && itemType == ItemType.statuette)
        {
            communicationManager.EnableMessage(messageNumber);
            communicationManager.ChangeText(messageNumber, "Zebrano statuetkê");
            player.collectedStatuettes += 1;
            saveGameManager.pickupNames.Add(this.name);
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
                communicationManager.ChangeText(messageNumber, "Zebrano Klucz");
                player.collectedKeys += 1;
                saveGameManager.keyNames.Add(this.name);
                communicationManager.ChangeText(keyCollectedText, $"{player.collectedKeys}/{player.maxCollectedKeys}");
                communicationManager.DisableMessageCourotine(messageNumber,3);

                string name = Array.Find(positionNames, s => s.Contains(this.name));

                this.transform.position = GameObject.Find(name).transform.position;
                this.transform.SetParent(GameObject.Find(name).transform.parent);
                Destroy(this.GetComponent<CollectableItem>());
                DisableBarrierScript.OpenBarrier(player.collectedKeys);
                //Destroy(gameObject);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        communicationManager.DisableMessage(pickObjectCommunicate);
    }

    


}
