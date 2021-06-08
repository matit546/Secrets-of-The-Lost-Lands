using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSovledScript : MonoBehaviour
{
    public List<string> names = new List<string>();
    public bool skonczony = false;
    public RiseStairs riseStairs;
    public void CheckItem()
    {
        int i = 0;
        int correctItem = 0;
        if (!skonczony)
        {
            foreach (Transform obj in this.transform)
            {
                if (obj.GetChild(1).childCount > 0)
                {
                    if (obj.GetChild(1).GetChild(0).name == names[i])
                    {
                        correctItem++;
                    }
                    i++;
                }
            }

            if (correctItem == 4)
            {
                skonczony = true;
                riseStairs.RiseStairsAnim();
            }
        }
    }
}
