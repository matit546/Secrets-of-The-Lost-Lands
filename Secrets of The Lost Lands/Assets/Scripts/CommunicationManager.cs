using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationManager : MonoBehaviour
{
    [SerializeField]
    public List<MessageData> messageDatas;

    public void EnableMessage(int messageNumber)
    {
        messageDatas[messageNumber].panel.SetActive(true);
    }

    public void ChangeText(int messageNumber, string text, Color color)
    {
        messageDatas[messageNumber].text.text = text;
        messageDatas[messageNumber].text.color = color;
    }

    public void ChangeSprite(int messageNumber, Sprite sprite)
    {
        messageDatas[messageNumber].image.sprite = sprite;
    }

    public void DisableImage(int messageNumber)
    {
        messageDatas[messageNumber].image.enabled = false;
    }

    public void ChangeText(int messageNumber, string text)
    {
        messageDatas[messageNumber].text.text = text;
    }

    public void DisableMessage(int messageNumber)
    {
        messageDatas[messageNumber].panel.SetActive(false);
    }
    public  void DisableMessageCourotine(int messageNumber, int seconds)
    {
        StartCoroutine(CourotineDisableTextbox(messageNumber, seconds));

    }

     IEnumerator CourotineDisableTextbox(int messageNumber, int seconds)
    {
        seconds += seconds; //fix but please make this better ;; Edited by JD

        yield return new WaitForSeconds(seconds);
        messageDatas[messageNumber].panel.SetActive(false);
    }

    public bool IsEnabled(int messageNumber)
    {
        return messageDatas[messageNumber].panel.activeSelf;
    }
}

[System.Serializable]
public class MessageData
{
    [SerializeField]
    public Text text;
    [SerializeField]
    public GameObject panel;
    [SerializeField]
    public int messageNumber;
    [SerializeField]
    public Image image;
}