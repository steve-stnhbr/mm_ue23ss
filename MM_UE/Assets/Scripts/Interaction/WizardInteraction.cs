using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardInteraction : MonoBehaviour, IDisableInputForMenu
{
    [SerializeField] float interactDelay = 0.2f;
    float lastInteractTime = 0;
    bool inputForMenuDisabled = false;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] float promptHeight = 1;

    private void OnTriggerStay(Collider other)
    {
        if (Time.time - lastInteractTime > interactDelay && Input.GetAxis("WizardInteract2") > 0.5f && !inputForMenuDisabled)
        {
            Debug.Log("WizardInteract2");
            lastInteractTime = Time.time;
            IInteractable otherScript = other.GetComponent<IInteractable>();
            Debug.Log(other.name);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() != null && other.GetComponent<Interactable>().isWizardInteractable)
        {
            GameObject prompt = Instantiate(interactPrompt);
            prompt.transform.SetParent(other.transform);
            Vector3 pos = other.transform.position;
            prompt.transform.position = new Vector3(pos.x, pos.y + promptHeight, pos.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            InteractPromptWizard prompt = other.gameObject.GetComponentInChildren<InteractPromptWizard>();
            if (prompt != null)
            {
                prompt.destroyPrompt();
            }
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
