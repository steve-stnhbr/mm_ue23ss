using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour, IHumanInteractable, IWizardInteractable
{
    public bool state = false;
    public Collider trigger;
    public Material materialWhenActive;
    Material materialWhenInactive;
    MeshRenderer parentMeshRenderer;

    private void Start()
    {
        parentMeshRenderer = GetComponentInParent<MeshRenderer>();
        materialWhenInactive = parentMeshRenderer.material;
        
    }

    public void HumanInteract()
    {
        Activate();
    }

    public void WizardInteract()
    {
        Activate();
    }

    void Activate()
    {
        state = !state;
        if (state)
        {
            parentMeshRenderer.material = materialWhenActive;
        }
        else
        {
            parentMeshRenderer.material = materialWhenInactive;
        }
    }



}
