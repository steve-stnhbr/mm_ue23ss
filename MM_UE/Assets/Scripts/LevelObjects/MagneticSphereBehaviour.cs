using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagneticSphereBehaviour : MonoBehaviour
{
    AudioSource rolling;
    public AudioClip drop;

    // Start is called before the first frame update
    void Start()
    {
        rolling = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        float volume = Sigmoid(collision.impulse.magnitude);
        Debug.Log("Drop volume: " + volume);
        rolling.PlayOneShot(drop, volume);
    }

    public void OnCollisionStay(Collision collision)
    {
        Vector3 velocityVec = GetComponent<Rigidbody>().velocity;
        float velocity = new Vector2(velocityVec.x, velocityVec.z).magnitude;

        rolling.volume = Mathf.Clamp(CalculateVolume(velocity), 0f, .8f);
        if (velocity > .5f)
        {
            rolling.pitch = 1 + velocity / 40;
        }
    }

    float Sigmoid(float value)
    {
        return 1.0f / (1.0f + (float)Math.Exp(-value));
    }

    float CalculateVolume(float value)
    {
        return Mathf.Exp(Mathf.Pow(value / 4f, 2)) - 1;
    }

    private void OnDestroy()
    {
        rolling.loop = false;
    }
}
