using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTerminal : Switch
{
    [Tooltip("When set, the object gets activated")]
    public Interactable objectToActivate;
    public Interactable objectToActivate2;
    [SerializeField] AudioClip activationSound;
    AudioSource audioSource;
    [SerializeField] GameObject light;
    [SerializeField] Material greenLight;
    Material oldMaterial;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        oldMaterial = light.GetComponent<MeshRenderer>().material;
    }

    protected override void DoWhileOffFixed()
    {

    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        light.GetComponent<MeshRenderer>().material = oldMaterial;
        audioSource.PlayOneShot(activationSound, 0.8f);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
        if (objectToActivate2 != null)
        {
            objectToActivate2.Interact(EnumActor.Script);
        }
    }

    protected override void SwitchOn(EnumActor actor)
    {
        light.GetComponent<MeshRenderer>().material = greenLight;
        audioSource.PlayOneShot(activationSound, 0.8f);
        if (objectToActivate != null)
        {
            objectToActivate.Interact(EnumActor.Script);
        }
        if (objectToActivate2 != null)
        {
            objectToActivate2.Interact(EnumActor.Script);
        }
    }

}
