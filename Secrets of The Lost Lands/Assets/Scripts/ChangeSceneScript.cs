using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    //public GameObject LoadingScreen;
    private int scene;

    public void LoadGame(int Scene)
    {
        List<AsyncOperation> LoadingScenes = new List<AsyncOperation>();
        //LoadingScreen.SetActive(true);

        LoadingScenes.Add(SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Additive));
        scene = Scene;
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
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(scene));

    }
}
