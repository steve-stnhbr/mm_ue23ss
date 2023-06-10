using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagneticSphereBehaviour : MonoBehaviour
{
    AudioSource drop, rolling;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        drop = audioSources[0];
        rolling = audioSources[1];
        foreach(AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "MetalBallHit")
            {
                drop = audioSource;
            } 
            else if (audioSource.clip.name == "MetalBallRolling")
            {
                rolling = audioSource;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        float volume = Sigmoid(Mathf.Log(collision.impulse.magnitude));
        drop.volume = volume;
        drop.pitch = UnityEngine.Random.Range(-1, 1);
        drop.Play();
    }

    public void OnCollisionStay(Collision collision)
    {
        float velocity = GetComponent<Rigidbody>().velocity.magnitude;
        if (velocity > 0)
        {
            rolling.volume = .2f + velocity / 100;
            rolling.pitch = 1 + velocity / 100;
        } else
        {
            rolling.volume = 0;
        }
    }

    float Sigmoid(float value)
    {
        return 1.0f / (1.0f + (float)Math.Exp(-value));
    }
}
