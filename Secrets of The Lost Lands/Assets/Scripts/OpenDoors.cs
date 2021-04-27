using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public Transform door;
    private bool isOpen = false;

    public void OpenCloseDoor()
    {

        if (isOpen)
        {
            door.Translate(0, -2f, 0);
            isOpen = false;
        }
        else
        {
            door.Translate(0, 2f, 0);
            isOpen = true;
        }
        
    }
}
