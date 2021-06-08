using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBarrierScript : MonoBehaviour
{
    public void OpenBarrier(int keys)
    {
        if(keys == 4)
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().collectedKeys == 4)
        {
            Destroy(this.gameObject);
        }
    }
}
