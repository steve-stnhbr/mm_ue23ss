using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardInteraction : MonoBehaviour, IDisableInputForMenu
{
    public float interactDelay = 0.2f;
    float lastInteractTime = 0;
    bool inputForMenuDisabled = false;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(Input.GetAxis("WizardInteract"));
        if (Time.time - lastInteractTime > interactDelay && Input.GetAxis("WizardInteract") > 0.5f && !inputForMenuDisabled)
        {
            lastInteractTime = Time.time;
            IInteractable otherScript = other.GetComponent<IInteractable>();
            if (otherScript != null)
            {
                otherScript.Interact(EnumActor.Wizard);
            }
        }
        IInteractableOnCollision otherScriptOnCollision = other.GetComponent<IInteractableOnCollision>();
        if (otherScriptOnCollision != null)
        {
            otherScriptOnCollision.Collide(EnumActor.Wizard);
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
