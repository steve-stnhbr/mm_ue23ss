using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextTerminalUI : MonoBehaviour
{
    [SerializeField] TextTerminal textTerminal;
    TextField textField;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonEnter = root.Q<Button>("ButtonEnter");
        textField = root.Q<TextField>("TextFieldPassword");

        buttonEnter.clicked += () => textTerminal.EnterPassword();
        textField.maxLength = textTerminal.getPasswordLength();
    }

    public string getText()
    {
        return textField.value;

    }

    public void setText(string text)
    {
        textField.value = text;
    }
}
