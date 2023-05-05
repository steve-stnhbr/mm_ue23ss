using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : Interactable
{
    [Header("Switch")]
    public bool state = false;
    [Tooltip("Delay between activations in seconds")]
    [Range(0f, float.MaxValue)]
    public float delayBetweenActivations = 0.2f;

    protected float lastActivationTime = 0f;

    // called only when the actor can interact with the object
    protected override void Activate(EnumActor actor)
    {
        // check time between activations
        if(Time.time-lastActivationTime >= delayBetweenActivations)
        {
            // change state
            state = !state;
            if (state)
            {
                SwitchOn(actor);
            }
            else
            {
                SwitchOff(actor);
            }

            lastActivationTime = Time.time;
        }
        
    }

    protected void FixedUpdate()
    {
        if (state)
        {
            DoWhileOnFixed();
        }
        else
        {
            DoWhileOffFixed();
        }
    }

    // called when state changes to true
    protected abstract void SwitchOn(EnumActor actor);

    // called every Fixed Update while switch is on
    protected abstract void DoWhileOnFixed();

    // called when state changes to false
    protected abstract void SwitchOff(EnumActor actor);

    // called every Fixed Update while switch is off
    protected abstract void DoWhileOffFixed();

}
