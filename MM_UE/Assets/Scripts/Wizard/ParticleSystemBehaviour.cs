using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemBehaviour : MonoBehaviour
{
    ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        particleSystem.GetParticles(particles);

        for (int i = 0; i < particleSystem.particleCount; i++)
        {
            particles[i].velocity = transform.TransformDirection(particles[i].velocity);
        }

        particleSystem.SetParticles(particles);
    }
}
