using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameMenu gameMenu;

    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonNewGame = root.Q<Button>("ButtonNewGame");
        Button buttonLevelSelector = root.Q<Button>("ButtonLevelSelector");
        Button buttonQuit = root.Q<Button>("ButtonQuit");

        buttonNewGame.clicked += () => gameMenu.loadLevel(1);
        buttonLevelSelector.clicked += () => gameMenu.openLevelSelector();
        buttonQuit.clicked += () => Application.Quit();
        buttonQuit.clicked += () => Debug.Log("Quit does not work in the editor");
    }
}
