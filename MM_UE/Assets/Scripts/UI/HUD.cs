using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] GameMenu gameMenu;

    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonReset = root.Q<Button>("ButtonReset");
        Button buttonMenu = root.Q<Button>("ButtonMenu");

        buttonReset.clicked += () => gameMenu.restartLevel();
        buttonMenu.clicked += () => gameMenu.togglePauseMenu();
    }
}
