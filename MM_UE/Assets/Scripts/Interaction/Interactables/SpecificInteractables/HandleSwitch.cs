using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSwitch : Switch
{
    [Tooltip("When set, the object gets activated")]
    public Interactable objectToActivate;
    [SerializeField]
    Animator animator;

    protected override void DoWhileOffFixed()
    {

    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        animator.SetBool("Active", false);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }

    protected override void SwitchOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }

}
