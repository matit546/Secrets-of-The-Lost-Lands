using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    private GameObject player;
    string[] positionNames = { "PositionKey1", "PositionKey2", "PositionKey3", "PositionKey4" };
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
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<Transform>().position = new Vector3(save.positionX, save.positionY, save.positionZ);
            player.GetComponent<CharacterController>().enabled = true;
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
            string name = Array.Find(positionNames, s => s.Contains(key));
            GameObject keyObj = GameObject.Find(key);
            keyObj.transform.position = GameObject.Find(name).transform.position;
            keyObj.transform.SetParent(GameObject.Find(name).transform.parent);
            //GameObject.Find(key).SetActive(false);
            player.GetComponent<Player>().collectedKeys += 1;
            GameObject UIManager = GameObject.Find("UIController");
            CommunicationManager communicationManager = UIManager.GetComponent<CommunicationManager>();
            int collectedKeys = player.GetComponent<Player>().collectedKeys;
            communicationManager.ChangeText(1, $"{player.GetComponent<Player>().collectedKeys}/{player.GetComponent<Player>().maxCollectedKeys}");
            Destroy(keyObj.GetComponent<CollectableItem>());
        }
        GameObject.Find("EndGameBarrier").GetComponent<DisableBarrierScript>().OpenBarrier(keyNames.Count);
    }

    public void DisableGottenPickups(List<string> pickupNames)
    {
        foreach (string pickup in pickupNames)
        {
            GameObject.Find(pickup).SetActive(false);
        }
    }
}
