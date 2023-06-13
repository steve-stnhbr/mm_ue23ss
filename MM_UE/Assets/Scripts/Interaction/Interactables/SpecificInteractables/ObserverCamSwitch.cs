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
    [SerializeField] float idleSoundIntervall = 3;
    [SerializeField] AudioClip idleSound;
    [SerializeField] GameObject camera;
    float lastIdleSoundTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void DoWhileOffFixed()
    {
        if (Time.time - lastIdleSoundTime > idleSoundIntervall)
        {
            lastIdleSoundTime = Time.time;
            audioSource.PlayOneShot(idleSound);
        }
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
