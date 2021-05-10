using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    [SerializeField]
    public List<DoorClass> doors;

    public void OpenCloseDoor()
    {
        foreach(DoorClass door in doors)
        {
            if (door.doorPosition.isOpen)
            {
                StartCoroutine(CloseDoor(door));
                door.doorPosition.isOpen = false;
            }
            else
            {
                StartCoroutine(OpenDoor(door));
                door.doorPosition.isOpen = true;
            }
        }
    }

    public IEnumerator OpenDoor(DoorClass door)
    {
        for (int i = 0; i < 100; i++)
        {
            door.transform.Translate(0, door.doorOpenRange / 100, 0);
            yield return null;
        }
    }

    public IEnumerator CloseDoor(DoorClass door)
    {
        for (int i = 0; i < 100; i++)
        {
            door.transform.Translate(0, -door.doorOpenRange / 100, 0);
            yield return null;
        }
    }

    [System.Serializable]
    public class DoorClass
    {
        [SerializeField]
        public Transform transform;
        [SerializeField]
        public DoorPosition doorPosition;
        [SerializeField]
        public float doorOpenRange = 3;
    }
}
