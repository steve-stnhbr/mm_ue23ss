using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenu : MonoBehaviour
{
    [Tooltip("Object reference of a GameObject with the GameMenuUI")]
    [SerializeField] GameObject mainMenuUI;
    MainMenuUI menuScript;
    [Tooltip("Object reference of a GameObject with the LoadingScreenUI")]
    [SerializeField] GameObject loadingScreenUI;
    LoadingScreenUI loadingScreenScript;
    [Tooltip("Object reference of a GameObject with the LevelSelectUI")]
    [SerializeField] GameObject levelSelectUI;
    LevelSelectUI levelSelectScript;
    [Tooltip("Object reference of a GameObject with the PauseUI")]
    [SerializeField] GameObject pauseUI;
    PauseMenuUI pauseScript;

    [Tooltip("if true the menu starts with the main menu and doesnt allow pausing, if false it doesnt show ui and allows pausing")]
    [SerializeField] bool isStartMenu = false;

    float lastPauseToggle = 0;

    private void OnEnable()
    {
        menuScript = mainMenuUI.GetComponent<MainMenuUI>();
        loadingScreenScript = loadingScreenUI.GetComponent<LoadingScreenUI>();
        levelSelectScript = levelSelectUI.GetComponent<LevelSelectUI>();
        pauseScript = pauseUI.GetComponent<PauseMenuUI>();

        if (isStartMenu)
        {
            mainMenuUI.SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetAxis("Menu Button")>0 && !isStartMenu && Time.time - lastPauseToggle > 0.3)
        {
            lastPauseToggle = Time.time;
            bool wasPauseActive = pauseUI.active;
            if (wasPauseActive)
            {
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = 1;
            }
            closeMenus();
            pauseUI.SetActive(!wasPauseActive);
        }
    }

    public void resumeGame()
    {
        if (!isStartMenu)
        {
            closeMenus();
            Time.timeScale = 1;
        }
    }

    public void returnToMenu()
    {
        closeMenus();
        if (isStartMenu)
        {
            mainMenuUI.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    public void openLevelSelector()
    {
        closeMenus();
        levelSelectUI.SetActive(true);
    }

    public void loadLevel(int level)
    {
        try
        {
            loadLevel(LevelManager.levelNames[level-1]);
        } 
        catch(IndexOutOfRangeException e)
        {
            Debug.Log("Level with index " + (level-1) + " does not exist in LevelManager");
            Debug.Log(e.Message);
        }
    }

    public void loadLevel(string levelToLoad)
    {
        closeMenus();
        loadingScreenUI.SetActive(true);
        StartCoroutine(loadLevelASync(levelToLoad));
    }
    

    IEnumerator loadLevelASync(string levelToLoad)
    {
        loadingScreenScript.updateLevelTitle(levelToLoad);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingScreenScript.updateProgress(progress);
            yield return null;
        }
    }

    private void closeMenus()
    {
        mainMenuUI.SetActive(false);
        loadingScreenUI.SetActive(false);
        levelSelectUI.SetActive(false);
        pauseUI.SetActive(false);
    }
}
