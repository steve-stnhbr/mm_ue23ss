using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSwitch : Switch
{
    [Tooltip("When set, the object gets activated")]
    public Interactable objectToActivate;
    public Interactable objectToActivate2;
    [SerializeField] Animator animator;
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

    protected override void SwitchOff(EnumActor actor)
    {
        animator.SetBool("Active", false);
        audioSource.PlayOneShot(activationSound, 0.8f);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
        if (objectToActivate2 != null)
        {
            objectToActivate2.Interact(EnumActor.Script);
        }
    }

    protected override void SwitchOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        audioSource.PlayOneShot(activationSound, 0.8f);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
        if (objectToActivate2 != null)
        {
            objectToActivate2.Interact(EnumActor.Script);
        }
    }

}
