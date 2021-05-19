using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCircle : MonoBehaviour
{
    public GameObject Circle;
    public bool isPuzzleEnded;
    public float angle = 90;
    public List<GameObject> goodPositions;
    public GameObject sphere;
    private bool clicked;
    CommunicationManager communicationManager;
    int messageNumberClickE = 0;
    int messageNumberCongratulation = 3;
    public Sprite sprite;
    private void Start()
    {
        communicationManager = GameObject.Find("UIController").GetComponent<CommunicationManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            communicationManager.EnableMessage(messageNumberClickE);
            communicationManager.ChangeText(messageNumberClickE, "Wcisnij");
            communicationManager.ChangeSprite(messageNumberClickE, sprite);
            if (Input.GetKey(KeyCode.E) && (!clicked))
            {
                clicked = true;
                _ = StartCoroutine(nameof(RefreshClick));
                RotateCircle();
            }
         
        }
   }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            communicationManager.DisableMessage(messageNumberClickE);
        }
    }

    void RotateCircle()
    {

        if (Circle.transform.eulerAngles.y >= 360)
        {
            Circle.transform.Rotate(0, 0, 0);
        }
        else
        {
     
            StartCoroutine(RotateAnimation());
        }
      
    }

    private void CheckifGood()
    {
        if (!isPuzzleEnded)
        {
            int counter = 0;
            goodPositions.ForEach(x =>
            {
                if (x.transform.eulerAngles.y >= -1 && x.transform.eulerAngles.y <= 0)
                {
                    counter++;
                }
            });

            if (counter == 4)
            {
                PuzzleSolvedGrats();
            }

        }
    }

    void PuzzleSolvedGrats()
    {
        sphere.SetActive(false);
        isPuzzleEnded = true;
        communicationManager.EnableMessage(messageNumberCongratulation);
        communicationManager.ChangeText(messageNumberCongratulation, "Gratulacje, zagadka rozwiazana!");
        communicationManager.DisableMessageCourotine(messageNumberCongratulation, 3);
    }
 
    public IEnumerator RotateAnimation()
    {
        for(int i=0; i < 90; i++)
        {
            yield return null;
            Circle.transform.Rotate(0, 1, 0);
        }
        clicked = false;
        CheckifGood();
    }

    public IEnumerator RefreshClick()
    {
        yield return new WaitForSeconds(1f);
    }

}
