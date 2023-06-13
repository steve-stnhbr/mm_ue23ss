using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResetableInteractable : Interactable
{
    
    public bool state = false;

    [Header("Timed Reset")]
    [Tooltip("When activated the button resets after the delay")]
    public bool hasTimedReset = false;
    [Tooltip("Reset delay after activation in seconds")]
    [Range(0f, float.MaxValue)]
    public float timedResetDelay = 2f;

    protected float lastActivationTime = 0f;

    // called only when the actor can interact with the object
    protected override void Activate(EnumActor actor)
    {
        if (!state)
        {
            lastActivationTime = Time.time;
            state = true;
            TurnOn(actor);
        }
    }

    protected void FixedUpdate()
    {
        // check time for reset
        if (state && hasTimedReset && Time.time - lastActivationTime >= timedResetDelay)
        {
            Reset();
        }

        if (state)
        {
            DoWhileOnFixed();
        } else
        {
            DoWhileOffFixed();
        }
    }

    // called when button turns on
    protected abstract void TurnOn(EnumActor actor);

    // called every Fixed Update while button is on
    protected abstract void DoWhileOnFixed();

    // called every Fixed Update while button is off
    protected abstract void DoWhileOffFixed();

    // called either after a delay after switching on or when the button should reset
    protected abstract void Reset();
}
