using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameMenu gameMenu;

    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonResume = root.Q<Button>("ButtonResume");
        Button buttonLevelSelector = root.Q<Button>("ButtonLevelSelector");
        Button buttonQuit = root.Q<Button>("ButtonQuit");

        buttonResume.clicked += () => gameMenu.resumeGame();
        buttonLevelSelector.clicked += () => gameMenu.openLevelSelector();
        buttonQuit.clicked += () => Application.Quit();
        buttonQuit.clicked += () => Debug.Log("Quit does not work in the editor");
    }
}
