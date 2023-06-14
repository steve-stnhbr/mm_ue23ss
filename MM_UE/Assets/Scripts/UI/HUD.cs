using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] GameMenu gameMenu;
    [TextArea(15, 20)]
    [SerializeField] string levelTip;

    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonReset = root.Q<Button>("ButtonReset");
        Button buttonMenu = root.Q<Button>("ButtonMenu");
        Label labelInfo = root.Q<Label>("LabelInfo");

        buttonReset.clicked += () => gameMenu.restartLevel();
        buttonMenu.clicked += () => gameMenu.togglePauseMenu();
        if(levelTip != null || !levelTip.Equals(""))
        {
            labelInfo.text = levelTip;
        }
    }
}
