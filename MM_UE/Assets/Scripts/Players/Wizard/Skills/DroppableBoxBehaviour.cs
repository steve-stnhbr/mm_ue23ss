using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DroppableBoxBehaviour : MonoBehaviour
{
    [Tooltip("This AduioClip is played when the box hits the floor")]
    public AudioClip dropClip;

    AudioSource audioSource;
    bool dropped;
    short framesAliveBeforeDrop;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dropped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dropped)
        {
            framesAliveBeforeDrop++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!dropped && framesAliveBeforeDrop > 3 && collision.gameObject.layer == LayerMask.NameToLayer("Ground") && GetComponent<Rigidbody>().velocity.magnitude < .1f)
        {
            dropped = true;
            this.playDropSound();
        }
    }

    private void playDropSound()
    {
        audioSource.PlayOneShot(dropClip);
    }
}
