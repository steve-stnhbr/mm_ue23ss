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
    [SerializeField] InputHandler inputHandler;
    [SerializeField] AudioClip processingClip;
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip errorClip;

    AudioSource audioSource;

    private TextTerminalUI terminalScript;
    private string terminalText = "";
    

    private void Start()
    {
        terminalScript = terminalUI.GetComponent<TextTerminalUI>();
        audioSource = GetComponent<AudioSource>();

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
        inputHandler.DisableInputForInteraction();
    }

    public void EnterPassword()
    {
        state = false;
        audioSource.PlayOneShot(processingClip);
        inputHandler.EnableInputForInteraction();
        terminalUI.SetActive(false);
        terminalText = terminalScript.getText();
        inWorldText.text = terminalText.ToUpper();

        StartCoroutine(processPassword());
    }

    public int getPasswordLength()
    {
        return password.Length;
    }

    IEnumerator processPassword()
    {
        yield return new WaitForSeconds(1);
        if (terminalText.ToLower().Equals(password.ToLower()))
        {
            audioSource.PlayOneShot(successClip);
            if (objectToActivate != null)
            {
                objectToActivate.Interact(EnumActor.Script);
            }

        }
        else
        {
            audioSource.PlayOneShot(errorClip);
        }
    }



}
