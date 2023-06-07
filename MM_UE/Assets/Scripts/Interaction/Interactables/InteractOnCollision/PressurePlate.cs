using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]

public class PressurePlate : InteractableOnCollision
{
    [Header("PressurePlate")]
    [SerializeField]
    [Tooltip("Should the plate stay active when activated once")]
    bool oneTimeUse = false;
    [Tooltip("When set, the object gets activated")]
    public Interactable objectToActivate;

    [SerializeField]
    Animator animator;

    bool state = false;

    private void OnTriggerExit(Collider other)
    {
        if(state && !oneTimeUse)
        {
            state = false;
            OnDeactivate();
        }
    }

    protected override void WhileCollision(EnumActor actor)
    {
        if (!state)
        {
            state = true;
            OnActivate();
        }
    }

    void OnActivate()
    {
        animator.SetBool("Active", true);
        if(objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }

    void OnDeactivate()
    {
        animator.SetBool("Active", false);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }


}
