using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]

public class PressurePlate : InteractableOnCollision
{
    [Header("PressurePlate")]
    [SerializeField]
    [Tooltip("Negative Height to where the plate moves, when activated")]
    float localDepressionOnActivate = 0.5f;
    [SerializeField]
    [Tooltip("Time in seconds the plate needs to fully depress")]
    float timeToDepress = 1f;
    [Tooltip("When active, the plate switches to this material, when inactive, reverts back")]
    public Material activeMaterial;
    [Tooltip("When set, the object gets activated")]
    public Interactable objectToActivate;
    


    Material inactiveMaterial;
    MeshRenderer meshRenderer;
    // Depression percent of the pressure plate [0-1]
    float depressionRate = 0;
    float maxDepressionHeight;
    float minDepressionHeight;
    bool state = false;

    protected void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        inactiveMaterial = meshRenderer.material;
        maxDepressionHeight = transform.localPosition.y;
        minDepressionHeight = maxDepressionHeight - localDepressionOnActivate;

    }

    protected void FixedUpdate()
    {
        UpdateDepression(Time.fixedDeltaTime / (-timeToDepress*2));
        UpdatePlateHeight();
    }

    protected override void WhileCollision(EnumActor actor)
    {
        UpdateDepression(Time.fixedDeltaTime / timeToDepress);
    }

    protected void UpdateDepression(float change)
    {
        depressionRate = Mathf.Clamp01(depressionRate + change);
        if (!state && depressionRate>0.75)
        {
            state = true;
            OnActivate();
        }
        else if (state && depressionRate < 0.75)
        {
            state = false;
            OnDeactivate();
        }
    }

    protected void UpdatePlateHeight()
    {
        float depressionHeight = maxDepressionHeight - (maxDepressionHeight - minDepressionHeight) * depressionRate;
        transform.localPosition = new Vector3(transform.localPosition.x, depressionHeight, transform.localPosition.z);
    }

    protected void OnActivate()
    {
        meshRenderer.material = activeMaterial;
        if(objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }

    protected void OnDeactivate()
    {
        meshRenderer.material = inactiveMaterial;
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
    }


}
