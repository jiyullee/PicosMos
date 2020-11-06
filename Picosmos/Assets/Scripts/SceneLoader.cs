using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private static int nextLoadSceneIndex = -1;
    private void Awake()
    {
        Instance = this;
        SceneManager.activeSceneChanged += SceneChange;
    }

    public static void LoadScene(int sceneIndex)
    {
        nextLoadSceneIndex = sceneIndex;
        SceneManager.LoadScene(1);
    }

    void SceneChange(Scene currentScene, Scene ChangeScene)
    {
        if (nextLoadSceneIndex != -1)
        {
            StartCoroutine(LoadSceneAsync(SceneManager.LoadSceneAsync(nextLoadSceneIndex)));
            nextLoadSceneIndex = -1;

        }
    }

    IEnumerator LoadSceneAsync(AsyncOperation asyncOperation)
    {
        yield return new WaitForSeconds(2.0f);

        while (!asyncOperation.isDone)
        {
            if (LoadingUI.Instance != null)
            {
                LoadingUI.Instance.Progress(asyncOperation.progress);
            }
            yield return null;
        }
    }
}
