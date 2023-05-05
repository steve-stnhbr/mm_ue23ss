using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardInteraction : MonoBehaviour
{
    public float interactDelay = 0.2f;
    float lastInteractTime = 0;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(Input.GetAxis("WizardInteract"));
        if (Time.time - lastInteractTime > interactDelay && Input.GetAxis("WizardInteract") > 0.5f)
        {
            lastInteractTime = Time.time;
            IInteractable otherScript = other.GetComponent<IInteractable>();
            if (otherScript != null)
            {
                otherScript.Interact(EnumActor.Wizard);
            }
        }

    }
}
