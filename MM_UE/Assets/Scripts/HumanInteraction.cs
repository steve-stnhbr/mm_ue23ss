using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInteraction : MonoBehaviour
{
    public float interactDelay = 0.2f;
    float lastInteractTime = 0;

    private void OnTriggerStay(Collider other)
    {
        if (Time.time-lastInteractTime>interactDelay && Input.GetAxis("HumanInteract") > 0.5f)
        {
            lastInteractTime = Time.time;
            IHumanInteractable otherScript = other.GetComponent<IHumanInteractable>();
            if (otherScript != null)
            {
                otherScript.HumanInteract();
            }
        }
        
    }
    
}
