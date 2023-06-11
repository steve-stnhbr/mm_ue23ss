using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInteraction : MonoBehaviour, IDisableInputForMenu
{
    public float interactDelay = 0.2f;
    float lastInteractTime = 0;
    bool inputForMenuDisabled = false;

    private void OnTriggerStay(Collider other)
    {
        if (Time.time-lastInteractTime>interactDelay && Input.GetAxis("HumanInteract") > 0.5f && !inputForMenuDisabled)
        {
            GetComponent<Animator>().SetBool("Interacts", true);

            lastInteractTime = Time.time;
            IInteractable otherScript = other.GetComponent<IInteractable>();
            if (otherScript != null)
            {
                otherScript.Interact(EnumActor.Human);
            }
        }
        IInteractableOnCollision otherScriptOnCollision = other.GetComponent<IInteractableOnCollision>();
        if (otherScriptOnCollision != null)
        {
            otherScriptOnCollision.Collide(EnumActor.Human);
        }

    }
    
    public void DisableInputForMenu()
    {
        inputForMenuDisabled = true;
    }

    public void EnableInputForMenu()
    {
        inputForMenuDisabled = false;
    }

}
