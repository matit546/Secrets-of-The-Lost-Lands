using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour
{
    public Image panel;
    public Text text;
    void Start()
    {

    }

    public void startCour()
    {
        StartCoroutine(ScreenTransition());
    }

    public IEnumerator ScreenTransition()
    {
        panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 100; i++)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a - (float)(1f / 100));
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (float)(1f / 100));
            yield return null;

        }
        panel.gameObject.SetActive(false);
    }
}
