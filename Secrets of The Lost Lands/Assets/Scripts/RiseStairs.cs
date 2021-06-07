using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseStairs : MonoBehaviour
{
    public void RiseStairsAnim()
    {
        float i = 0;
        foreach(Transform stair in this.transform)
        {
            StartCoroutine(Rise(stair, i));
            i++;
        }
    }

    public IEnumerator Rise(Transform stair, float multi)
    {
        for (int i = 0; i < 100; i++)
        {
            stair.transform.Translate(0, 0.08f + (0.005f * multi), 0);
            yield return null;
        }
    }
}
