using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalActivatorSwitch : Switch
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject LevelEndObject;

    new public bool state
    {
        get { return state; }
        set { state = value; GetComponent<Animator>().SetBool("Active", true); }
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
    }

    protected override void SwitchOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        LevelEndObject.SetActive(true);
    }


}
