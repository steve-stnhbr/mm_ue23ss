using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverCamSwitch : Switch
{
    [Tooltip("When set, the object gets activated")]
    public GameObject objectToActivate;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip activationSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void DoWhileOffFixed()
    {

    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        animator.SetBool("Active", false);
        audioSource.PlayOneShot(activationSound, 0.8f);
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    protected override void SwitchOn(EnumActor actor)
    {
        animator.SetBool("Active", true);
        audioSource.PlayOneShot(activationSound, 0.8f);
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
