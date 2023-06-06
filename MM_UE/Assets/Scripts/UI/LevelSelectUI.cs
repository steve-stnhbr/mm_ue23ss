using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] GameMenu gameMenu;
    int levelCount = 12;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonBack = root.Q<Button>("ButtonBack");
        buttonBack.clicked += () => gameMenu.returnToMenu();

        Button[] levelButtons = new Button[levelCount];
        for(int i = 0; i < levelCount; i++)
        {
            if(root.Q<Button>("ButtonLevel" + (i+1)) != null)
            {
                levelButtons[i] = root.Q<Button>("ButtonLevel" + (i+1));
                int index = i+1;
                levelButtons[i].clicked += () => gameMenu.loadLevel(index);
            }
        }
        

    }
}
