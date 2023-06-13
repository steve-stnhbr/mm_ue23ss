using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] GameMenu gameMenu;

    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonRestart = root.Q<Button>("ButtonRestart");
        Button buttonLevelSelector = root.Q<Button>("ButtonLevelSelector");
        Button buttonQuit = root.Q<Button>("ButtonQuit");

        buttonRestart.clicked += () => gameMenu.restartLevel();
        buttonLevelSelector.clicked += () => gameMenu.openLevelSelector();
        buttonQuit.clicked += () => Application.Quit();
        buttonQuit.clicked += () => Debug.Log("Quit does not work in the editor");

    }

}
