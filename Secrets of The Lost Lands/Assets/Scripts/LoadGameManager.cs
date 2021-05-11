using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    private GameObject player;
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + $"/gamesave.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + $"/gamesave.txt", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            player = GameObject.Find("MainCharacter");

            //Load save
            player.GetComponent<Transform>().position = new Vector3(save.positionX, save.positionY, save.positionZ);
            DisableGottenKeys(save.keyNames);
            DisableGottenPickups(save.pickupNames);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void DisableGottenKeys(List<string> keyNames)
    {
        foreach(string key in keyNames)
        {
            GameObject.Find(key).SetActive(false);
            player.GetComponent<Player>().collectedKeys += 1;
            GameObject UIManager = GameObject.Find("UIController");
            CommunicationManager communicationManager = UIManager.GetComponent<CommunicationManager>();
            int collectedKeys = player.GetComponent<Player>().collectedKeys;
            communicationManager.ChangeText(1, $"{player.GetComponent<Player>().collectedKeys}/{player.GetComponent<Player>().maxCollectedKeys}");
        }
    }

    public void DisableGottenPickups(List<string> pickupNames)
    {
        foreach (string pickup in pickupNames)
        {
            GameObject.Find(pickup).SetActive(false);
        }
    }
}
