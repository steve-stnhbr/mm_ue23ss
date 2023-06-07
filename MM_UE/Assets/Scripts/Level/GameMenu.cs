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
    [Tooltip("Object reference of a GameObject with the HUD")]
    [SerializeField] GameObject hud;
    HUD hudScript;


    [Tooltip("if true the menu starts with the main menu and doesnt allow pausing, if false it doesnt show ui and allows pausing")]
    [SerializeField] bool isStartMenu = false;

    float lastPauseToggle = 0;

    private void OnEnable()
    {
        menuScript = mainMenuUI.GetComponent<MainMenuUI>();
        loadingScreenScript = loadingScreenUI.GetComponent<LoadingScreenUI>();
        levelSelectScript = levelSelectUI.GetComponent<LevelSelectUI>();
        pauseScript = pauseUI.GetComponent<PauseMenuUI>();
        hudScript = hud.GetComponent<HUD>();

        if (isStartMenu)
        {
            mainMenuUI.SetActive(true);
        } else
        {
            hud.SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetAxis("MenuButton")>0 && !isStartMenu && Time.time - lastPauseToggle > 0.3)
        {
            togglePauseMenu();
        }
        if (Input.GetAxis("ResetButton") > 0 && !isStartMenu && Time.time - lastPauseToggle > 0.3)
        {
            restartLevel();
        }
    }

    public void togglePauseMenu()
    {
        lastPauseToggle = Time.time;
        bool wasPauseActive = pauseUI.active;
        closeMenus();
        hud.SetActive(wasPauseActive);
        pauseUI.SetActive(!wasPauseActive);
    }

    public void restartLevel()
    {
        if (!isStartMenu)
        {
            loadLevel(SceneManager.GetActiveScene().name);
        }
    }

    public void resumeGame()
    {
        if (!isStartMenu)
        {
            closeMenus();
            hud.SetActive(true);
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
        hud.SetActive(false);
    }
}
