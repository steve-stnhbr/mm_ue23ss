using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereActivatorBehaviour : Switch
{
    public Interactable objectToActivate;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        ActivationBehaviour activation;
        float distance = Vector3.Distance(collision.gameObject.transform.position, GetComponent<Collider>().bounds.center);
        if (collision.gameObject.TryGetComponent(out activation) && distance < .8)
        {
            if (!this.state)
            {
                this.Activate(EnumActor.Object);
            }
        } 
        else
        {
            //this.Activate(EnumActor.Object);
        }
    }

    protected override void SwitchOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        objectToActivate.Interact(EnumActor.Object);
    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        animator.SetBool("Active", false);
        objectToActivate.Interact(EnumActor.Object);
    }

    protected override void DoWhileOffFixed()
    {
    }
}
