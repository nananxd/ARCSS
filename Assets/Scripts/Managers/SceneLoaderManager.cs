using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoaderManager : MonoBehaviour
{
    [SerializeField] private float minimumLoadingTime;

    public void LoadScene(string sceneIndex,RectTransform loadingRect)
    {
        StartCoroutine(LoadSceneRoutine(sceneIndex, loadingRect));
    }

    private IEnumerator LoadSceneRoutine(string sceneIndex,RectTransform loadingRect)
    {
        float startTime = Time.time;

        // Start asynchronous loading
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Prevent the scene from activating immediately
        loadingRect.localScale = Vector3.one;
        operation.allowSceneActivation = false;

        // Wait until loading is complete
        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        // Wait until minimum loading time has elapsed
        float elapsedTime = Time.time - startTime;

        if (elapsedTime < minimumLoadingTime)
        {
            yield return new WaitForSeconds(minimumLoadingTime - elapsedTime);
        }

        // Activate the scene
        operation.allowSceneActivation = true;
    }
}
