using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInteraction : MonoBehaviour, IDisableInputForMenu
{
    [SerializeField] float interactDelay = 0.2f;
    float lastInteractTime = 0;
    bool inputForMenuDisabled = false;

    [SerializeField] GameObject interactUI;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IInteractable>() != null)
        {
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            interactUI.SetActive(false);
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
