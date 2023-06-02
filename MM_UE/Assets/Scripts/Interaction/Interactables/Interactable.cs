using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [Header("Actors")]
    public bool isHumanInteractable = true;
    public bool isWizardInteractable = true;
    public bool isObjectInteractable = false;
    [Header("Interactable")]
    public Collider triggerCollider;

    // called by any actor that interacts
    public void Interact(EnumActor actor)
    {
        // cancel if actor should not be able to interact
        if((!isHumanInteractable && actor == EnumActor.Human) ||
            (!isWizardInteractable && actor == EnumActor.Wizard) ||
            (!isObjectInteractable && actor == EnumActor.Object))
        { 
            return;
        }

        Activate(actor);
    }

    // called only when the actor can interact with the object
    protected abstract void Activate(EnumActor actor);
}
