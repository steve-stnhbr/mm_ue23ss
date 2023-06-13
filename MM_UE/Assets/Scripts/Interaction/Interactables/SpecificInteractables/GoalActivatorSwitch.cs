using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalActivatorSwitch : Switch
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject LevelEndObject;
    [SerializeField] AudioClip activationSound;
    [SerializeField] AudioClip deactivationSound;
    [SerializeField] AudioClip portalSound;
    AudioSource audioSource;
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = portalSound;
    }


    new public bool state
    {
        get { return state; }
        set { state = value;
            if (state)
            {
                SwitchOn(EnumActor.Script);
            }
            else
            {
                SwitchOff(EnumActor.Script);
            }
            
            }
    }

    protected override void DoWhileOffFixed()
    {
        animator.SetBool("Active", false);
        LevelEndObject.SetActive(false);
    }

    protected override void DoWhileOnFixed()
    {
        animator.SetBool("Active", true);
        LevelEndObject.SetActive(true);
    }

    protected override void SwitchOff(EnumActor actor)
    {
        animator.SetBool("Active", false);
        LevelEndObject.SetActive(false);
        audioSource.Stop();
        audioSource.PlayOneShot(deactivationSound);
    }

    protected override void SwitchOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        LevelEndObject.SetActive(true);
        audioSource.PlayOneShot(activationSound);
        audioSource.PlayDelayed(activationSound.length);
    }


}
