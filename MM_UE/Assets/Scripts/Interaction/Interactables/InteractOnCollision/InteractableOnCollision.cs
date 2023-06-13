using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]


// this is for Physical activation like pressure plates
public abstract class InteractableOnCollision : MonoBehaviour, IInteractableOnCollision
{
    [Header("Actors")]
    public bool isHumanInteractable = true;
    public bool isWizardInteractable = true;
    public bool isObjectInteractable = false;
    [Header("Interactable")]
    public Collider triggerCollider;

    // called by any actor that interacts
    public void Collide(EnumActor actor)
    {
        // cancel if actor should not be able to interact
        if ((!isHumanInteractable && actor == EnumActor.Human) ||
            (!isWizardInteractable && actor == EnumActor.Wizard) ||
            (!isObjectInteractable && actor == EnumActor.Object))
        {
            return;
        }

        WhileCollision(actor);
    }

    // called only when the actor can interact with the object
    protected abstract void WhileCollision(EnumActor actor);


}
