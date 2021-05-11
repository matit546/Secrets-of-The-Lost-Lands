using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public Transform player;
    public List<string> keyNames;
    public List<string> pickupNames;

    public void SaveGame()
    {
        Save save = SaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + $"/gamesave.txt");
        Debug.Log(Application.persistentDataPath + $"/gamesave.txt");
        bf.Serialize(file, save);
        file.Close();
    }

    private Save SaveGameObject()
    {
        Save save = new Save();

        //Save stuff
        save.positionX = player.position.x;
        save.positionY = player.position.y;
        save.positionZ = player.position.z;
        save.keyNames = keyNames;
        save.pickupNames = pickupNames;

        return save;
    }

    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}
}
