using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenu : MonoBehaviour, IDisableInputForInteraction
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
    [Tooltip("Object reference of a GameObject with the GameOverUI")]
    [SerializeField] GameObject gameOverUI;
    GameOverUI gameOVerScript;
    [Tooltip("Script reference the Input Handler")]
    [SerializeField] InputHandler inputHandler;

    bool inputDisabledForInteraction = false;


    [Tooltip("if true the menu starts with the main menu and doesnt allow pausing, if false it doesnt show ui and allows pausing")]
    [SerializeField] bool isStartMenu = false;

    bool isGameOver = false;
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
            inputHandler.DisableInputForMenu();

        } else
        {
            hud.SetActive(true);
            inputHandler.EnableInputForMenu();
        }
    }

    private void Update()
    {
        if(Input.GetAxis("MenuButton")>0 && !isStartMenu && Time.time - lastPauseToggle > 0.3)
        {
            togglePauseMenu();
        }
        if (Input.GetAxis("ResetButton") > 0 && !isStartMenu && Time.time - lastPauseToggle > 0.3 && !inputDisabledForInteraction)
        {
            restartLevel();
        }
    }

    public void togglePauseMenu()
    {
        if (isGameOver)
        {
            return;
        }
        lastPauseToggle = Time.time;
        bool wasPauseActive = pauseUI.active;
        closeMenus();
        if (wasPauseActive)
        {
            inputHandler.EnableInputForMenu();
        }else
        {
            inputHandler.DisableInputForMenu();
        }
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
            inputHandler.EnableInputForMenu();
        }
    }

    public void returnToMenu()
    {
        closeMenus();
        if (isStartMenu)
        {
            mainMenuUI.SetActive(true);
            inputHandler.DisableInputForMenu();
        }
        else
        {
            if (isGameOver)
            {
                gameOverUI.SetActive(true);
                inputHandler.DisableInputForMenu();
                return;
            } 
            pauseUI.SetActive(true);
            inputHandler.DisableInputForMenu();
        }
        
    }

    public void openGameOverMenu()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        closeMenus();
        gameOverUI.SetActive(true);
        inputHandler.DisableInputForMenu();

    }

    public void loadNextLevel()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        string[] levels = LevelManager.levelNames;
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].Equals(currentLevelName))
            {
                loadLevel(LevelManager.levelNames[i+1]);
                return;
            }
        }
        Debug.Log("Next level for '" + currentLevelName + "' not found");
    }

    public void openLevelSelector()
    {
        closeMenus();
        levelSelectUI.SetActive(true);
        inputHandler.DisableInputForMenu();
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
        Debug.Log("Loading level " + levelToLoad);
        
        string[] levels = LevelManager.levelNames;
        string loadingScreenLevelName = "";
        for(int i = 0; i<levels.Length; i++)
        {
            if (levels[i].Equals(levelToLoad))
            {
                loadingScreenLevelName = LevelManager.levelLoadingNames[i];
                break;
            }
        }
        loadingScreenScript.updateLevelTitle(loadingScreenLevelName);

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
        gameOverUI.SetActive(false);
    }

    public void DisableInputForInteraction()
    {
        inputDisabledForInteraction = true;
    }

    public void EnableInputForInteraction()
    {
        inputDisabledForInteraction = false;
    }
}
