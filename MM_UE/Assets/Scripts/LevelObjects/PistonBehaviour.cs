using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBehaviour : Switch
{
    public GameObject[] relativeGameObjects;

    public bool moveCollidedExplicitly;

    Vector3[] relativePositions;
    Vector3 positionBefore;

    Animator animator;

    protected override void DoWhileOffFixed()
    {
    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        Debug.Log("Off");
        animator.SetBool("Active", false);
    }

    protected override void SwitchOn(EnumActor actor)
    {
        Debug.Log("On");
        animator.SetBool("Active", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        relativePositions = new Vector3[relativeGameObjects.Length];
        for (int i = 0; i < relativeGameObjects.Length; i++)
        {
            relativePositions[i] = relativeGameObjects[i].transform.position - transform.position;
        }
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();
        if (transform.position != positionBefore)
        {
            for (int i = 0; i < relativeGameObjects.Length; i++)
            {
                relativeGameObjects[i].transform.position = relativePositions[i] + transform.position;
            }
        }
        positionBefore = transform.position;
    }
}
