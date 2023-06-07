using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalActivatorSwitch : Switch
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject LevelEndObject;

    protected override void DoWhileOffFixed()
    {
    }

    protected override void DoWhileOnFixed()
    {
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
