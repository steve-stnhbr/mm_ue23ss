using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInteraction : MonoBehaviour
{
    public Collision trigger;

    private void FixedUpdate()
    {
        if (Input.GetAxis("HumanInteract") > 0.5f)
        {
            
        }
    }
}
