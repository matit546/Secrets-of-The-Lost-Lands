using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonefireManager : MonoBehaviour
{
    public int messageNumberWithButton = 0;
    public int messageNumberWithoutButton = 2;
    private CommunicationManager communicationManager;
    public GameObject UIManager;
    public Sprite sprite;
    public SaveGameManager saveGameManager;
    bool didUse;

    private void Start()
    {
        communicationManager = UIManager.GetComponent<CommunicationManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        didUse = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!didUse)
        {
            communicationManager.EnableMessage(messageNumberWithButton);
            communicationManager.ChangeText(messageNumberWithButton, "Zapisz grê");
            communicationManager.ChangeSprite(messageNumberWithButton, sprite);

            if (Input.GetKey(KeyCode.E))
            {
                communicationManager.DisableMessage(messageNumberWithButton);
                communicationManager.EnableMessage(messageNumberWithoutButton);
                saveGameManager.SaveGame();
                communicationManager.ChangeText(messageNumberWithoutButton, "Zapisano");
                communicationManager.DisableMessageCourotine(messageNumberWithoutButton, 3);
                didUse = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        didUse = false;
        communicationManager.DisableMessage(messageNumberWithButton);
    }
}
