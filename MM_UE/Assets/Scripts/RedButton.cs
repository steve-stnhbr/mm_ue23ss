using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour, IHumanInteractable, IWizardInteractable
{
    public bool active = false;
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
        active = true;
    }

    void Update()
    {
        if (active)
        {
            parentMeshRenderer.material = materialWhenActive;
        }
        else
        {
            parentMeshRenderer.material = materialWhenInactive;
        }
        active = false;
    }


}
