 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    string[] colorTag = { "#FF0000", "#FFFFFF" };
    public bool isEnded;
    private bool clicked;
    static string currentText="";
    string correctText = "☾1▲1┌0█0┐0▲1☽1";
    static string helpText = "";
    TextMesh text;
    CommunicationManager communicationManager;
    int messageNumberClickE = 0;
    int messageNumberCongratulation = 3;
    static int color;
    public Sprite sprite;
    public GameObject planeRed, planeWhite;
    public Transform crate;
    void Start()
    {
        communicationManager = GameObject.Find("UIController").GetComponent<CommunicationManager>();
        text  = GameObject.Find("3DTEXT").GetComponent<TextMesh>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameObject.CompareTag("Column"))
        {
            text.gameObject.SetActive(true);
        }
 
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && gameObject.CompareTag("Column"))
        {
            communicationManager.EnableMessage(messageNumberClickE);
            communicationManager.ChangeText(messageNumberClickE, "Wcisnij");
            communicationManager.ChangeSprite(messageNumberClickE, sprite);
            if (Input.GetKey(KeyCode.E) && (!clicked))
            {
                clicked = true;
                SendValue(gameObject);
                _ = StartCoroutine(nameof(RefreshClick));
            }

            if (currentText == correctText && !isEnded)
            {
                communicationManager.EnableMessage(messageNumberCongratulation);
                communicationManager.ChangeText(messageNumberCongratulation, "Gratulacje, zagadka rozwiazana!");
                communicationManager.DisableMessageCourotine(messageNumberCongratulation, 3);
                StartCoroutine(OpenGate());
                isEnded = true;
                currentText = "";
            }

            if (helpText.Length > 7)
            {
                string lastTwoLetters = currentText.Substring(currentText.Length - 2);
                currentText = currentText[currentText.Length - 2].ToString();
                helpText = currentText;
                text.text = $"<color={colorTag[color]}>{currentText}</color>";
                currentText = lastTwoLetters;
            }

            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !gameObject.CompareTag("Column"))
        {
            text.gameObject.SetActive(false);
        }

        if (other.CompareTag("Player") && gameObject.CompareTag("Column"))
        {
            communicationManager.DisableMessage(messageNumberClickE);
        }
    }
   
    
    public void SendValue(GameObject gameobject)
    {

        char[] symbols = { '☾', '▲', '┌', '█', '┐', '☽' };
        switch (gameObject.name)
        {
            case "Column1":
                currentText +=  symbols[0] + color.ToString();
                helpText += symbols[0];
                text.text += $"<color={colorTag[color]}>{symbols[0]}</color>";
                break;
            case "Column2":
                currentText += symbols[1] + color.ToString();
                helpText += symbols[1];
                text.text += $"<color={colorTag[color]}>{symbols[1]}</color>";
                break;
            case "Column3":
                currentText += symbols[2] + color.ToString();
                helpText += symbols[2];
                text.text += $"<color={colorTag[color]}>{symbols[2]}</color>";
                break;
            case "Column4":
                currentText += symbols[3] + color.ToString();
                helpText += symbols[3];
                text.text += $"<color={colorTag[color]}>{symbols[3]}</color>";
                break;
            case "Column5":
                currentText += symbols[4] + color.ToString();
                helpText += symbols[4];
                text.text += $"<color={colorTag[color]}>{symbols[4]}</color>";
                break;
            case "Column6":
                currentText += symbols[5] + color.ToString();
                helpText += symbols[5];
                text.text += $"<color={colorTag[color]}>{symbols[5]}</color>";
                break;
            case "Column7":
                if (color == 0)
                {
                    planeWhite.SetActive(true);
                    planeRed.SetActive(false);
                    color = 1;
                }
                else
                {
                    planeWhite.SetActive(false);
                    planeRed.SetActive(true);
                    color = 0;
                }
                break;
            default:
                break;


        }

     
    }

    public IEnumerator RefreshClick()
    {
        yield return new WaitForSeconds(.5f);
        clicked = false;
    }

    public IEnumerator OpenGate()
    {
        for(int i = 0; i < 100; i++)
        {
            yield return null;
            crate.Translate(0, 0.05f, 0);
        }
    }


}
