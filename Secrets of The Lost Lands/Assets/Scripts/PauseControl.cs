using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{

    public static bool isGamePaused;
    public GameObject pauseMenuUI;
    public GameObject menuOptions;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused ^= true;

            if (isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
       
        }
    }

    void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }

   public void ShowOptions()
    {
        menuOptions.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void ResumeGame()
    {
        Cursor.visible = false;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

}
