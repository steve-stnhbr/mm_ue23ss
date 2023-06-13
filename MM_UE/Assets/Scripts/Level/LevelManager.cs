using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{


    public static string[] levelNames = new string[] {
        "CubeIntroLevel",
        "MagnetIntroLevel",
        "WaterLevel",
        "PistonPuzzle",
        "ImagePuzzle",
        "PasswordLevel",
        "MainMenu"
    };

    public static string[] levelLoadingNames = new string[] {
        "Cube Intro",
        "Magnet Intro",
        "Water Level",
        "Piston Level",
        "Image Level",
        "Password Level",
        "Main Menu"
    };

    public static int currentIndex = 0;

    public static int loadNextLevel()
    {
        SceneManager.LoadScene(levelNames[++currentIndex]);
        return currentIndex;
    }

    public static Level getCurrentLevel()
    {
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            Level l = go.GetComponent<Level>();
            if (l != null)
                return l;
        }
        return null;
    }
}
