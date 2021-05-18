using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int collectedStatuettes;
    public int collectedKeys;
    public int maxCollectedKeys = 4;
    public bool godMode;
    public float gameTime;

    private void Start()
    {
        gameTime = 0;
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
    }
}
