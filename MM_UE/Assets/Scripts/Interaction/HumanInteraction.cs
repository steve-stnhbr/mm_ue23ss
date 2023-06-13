using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInteraction : MonoBehaviour, IDisableInputForMenu
{
    [SerializeField] float interactDelay = 0.2f;
    float lastInteractTime = 0;
    bool inputForMenuDisabled = false;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] float promptHeight = 1;


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
        if(other.GetComponent<Interactable>() != null && other.GetComponent<Interactable>().isHumanInteractable)
        {
            GameObject prompt = Instantiate(interactPrompt);
            prompt.transform.SetParent(other.transform);
            Vector3 pos = other.transform.position;
            prompt.transform.position = new Vector3(pos.x, pos.y+promptHeight, pos.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            InteractPrompt prompt = other.gameObject.GetComponentInChildren<InteractPrompt>();
            if(prompt != null)
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
