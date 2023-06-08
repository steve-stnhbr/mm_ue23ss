using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTerminal : Switch
{
    [SerializeField] GameObject terminalUI;
    
    [SerializeField] string password;
    [SerializeField] TextMeshPro inWorldText;
    [SerializeField] Interactable objectToActivate;

    private TextTerminalUI terminalScript;
    private string terminalText = "";
    

    private void Start()
    {
        terminalScript = terminalUI.GetComponent<TextTerminalUI>();

    }

    protected override void DoWhileOffFixed()
    {

    }

    protected override void DoWhileOnFixed()
    {

    }

    protected override void SwitchOff(EnumActor actor)
    {
        EnterPassword();

    }

    protected override void SwitchOn(EnumActor actor)
    {
        terminalUI.SetActive(true);
        terminalScript.setText(terminalText.ToUpper());
    }

    public void EnterPassword()
    {
        terminalUI.SetActive(false);
        terminalText = terminalScript.getText();
        inWorldText.text = terminalText.ToUpper();
        if (terminalText.ToLower().Equals(password.ToLower()) && objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }

    public int getPasswordLength()
    {
        return password.Length;
    }



}
