using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutObjectHere : MonoBehaviour
{
    public Transform objectLocation;
    private Transform player;
    private PlayerRaycast playerRaycast;
    public GameObject Canvas;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRaycast = player.GetComponent<PlayerRaycast>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ShowAnim(Canvas.transform.GetChild(1).GetComponent<Text>(), 256f));
        StartCoroutine(ShowAnim(Canvas.transform.GetChild(0).GetComponent<Text>(), 256f));
        if (other.tag == "Player")
        {
            playerRaycast.EnablePutDown(objectLocation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(ShowAnim(Canvas.transform.GetChild(1).GetComponent<Text>(), -128f));
        StartCoroutine(ShowAnim(Canvas.transform.GetChild(0).GetComponent<Text>(), -128f));
        if (other.tag == "Player")
        {
            playerRaycast.DisablePutDown(objectLocation);
        }
    }

    public IEnumerator ShowAnim(Text text, float value)
    {
        for (int i = 1; i < (int)Mathf.Abs(value); i++)
        {
            text.GetComponent<Text>().color = new Color(text.GetComponent<Text>().color.r, text.GetComponent<Text>().color.g, text.GetComponent<Text>().color.b, text.GetComponent<Text>().color.a + (float)(1f / value));
            yield return null;
        }
    }
}
