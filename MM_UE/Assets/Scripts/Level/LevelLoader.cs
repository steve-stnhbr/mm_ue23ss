using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenUI;
    [SerializeField] private GameObject mainMenuUI;

    public void LoadLevel(string levelToLoad)
    {
        mainMenuUI.SetActive(false);
        loadingScreenUI.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));
    }


    IEnumerator LoadLevelASync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while(!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress/0.9f);
            // set slider value
            yield return null;
        }
    }
}
