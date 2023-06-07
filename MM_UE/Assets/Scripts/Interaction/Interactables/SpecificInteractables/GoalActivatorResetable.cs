using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalActivatorResetable : ResetableInteractable
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject LevelEndObject;

    protected override void DoWhileOffFixed()
    {
    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void Reset()
    {
        animator.SetBool("Active", false);
        LevelEndObject.SetActive(false);
    }

    protected override void TurnOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        LevelEndObject.SetActive(true);
    }
}
