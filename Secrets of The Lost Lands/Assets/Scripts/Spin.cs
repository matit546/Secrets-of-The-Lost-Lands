using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    int timer = 0;
    void FixedUpdate()
    {

        if(this.transform.rotation.y != 360f)
        {
            this.transform.Rotate(0f, 1f, 0f,Space.World);
        }
        else
        {
            this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }

        timer++;

        if (timer >= 0 && timer < 30)
        {
            this.transform.Translate(0f, 0.01f, 0f, Space.World);
        }
        else if(timer >= 30 && timer < 60)
        {
            this.transform.Translate(0f, -0.01f, 0f, Space.World);
        }
        else if(timer == 60)
        {
            timer = 0;
        }
    }
}
