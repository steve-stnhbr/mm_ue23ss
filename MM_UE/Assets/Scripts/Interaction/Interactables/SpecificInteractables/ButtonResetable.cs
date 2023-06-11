using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResetable : ResetableInteractable
{
    [SerializeField] Animator animator;
    public Interactable objectToActivate;
    [SerializeField] AudioClip activationSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void DoWhileOffFixed()
    {
        
    }

    protected override void DoWhileOnFixed()
    {
        
    }

    protected override void Reset()
    {
        animator.SetBool("Active", false);
        state = false;
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }

    protected override void TurnOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        audioSource.PlayOneShot(activationSound);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }

}
