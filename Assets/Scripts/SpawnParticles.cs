using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    private float spawnParticleInterval = 1f;
    private float spawnAntiParticleInterval = 2f;

    public GameObject particlePrefab;
    public GameObject antiParticlePrefab;

    private const int particlesPerSpawn = 5;
    private const int antiParticlesPerSpawn = 1;
    
    private const int maxParticles = 150;
    private const int maxAntiParticles = 50;

    void Start()
    {
        Instantiate(this.particlePrefab);
        Instantiate(this.antiParticlePrefab);

        StartCoroutine(this.spawnParticles());
        StartCoroutine(this.spawnAntiParticles());
    }

    void spawnParticle()
    {
        if (Particle.Particles != null && Particle.Particles.Count < SpawnParticles.maxParticles)
        {
            for (int i = 0; i < SpawnParticles.particlesPerSpawn; i++)
            {
                Instantiate(this.particlePrefab);
            }
        }
    }
    
    void spawnAntiParticle()
    {
        if (AntiParticle.AntiParticles != null && AntiParticle.AntiParticles.Count < SpawnParticles.maxAntiParticles)
        {
            for (int i = 0; i < SpawnParticles.antiParticlesPerSpawn; i++)
            {
                Instantiate(this.antiParticlePrefab);
            }
        }
    }

    IEnumerator spawnParticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.spawnParticleInterval);
            this.spawnParticle();
        }
    }
    
    IEnumerator spawnAntiParticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.spawnAntiParticleInterval);
            this.spawnAntiParticle();
        }
    }
}
