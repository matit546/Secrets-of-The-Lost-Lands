using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void BackToMenu()
    {
            Time.timeScale = 1;
        List<AsyncOperation> LoadingScenes = new List<AsyncOperation>();
        //LoadingScreen.SetActive(true);

        LoadingScenes.Add(SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(LoadingScenes));
    }

    public IEnumerator GetSceneLoadProgress(List<AsyncOperation> LoadingScenes)
    {

        foreach (var Scene in LoadingScenes)
        {
            while (!Scene.isDone)
            {
                Debug.Log("Not Loaded");
                yield return null;
            }
        }
        //LoadingScreen.SetActive(false);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
    }
}
