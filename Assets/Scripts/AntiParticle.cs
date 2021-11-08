using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiParticle : MonoBehaviour
{
    public static List<AntiParticle> AntiParticles;

    private static float G = 1200f;

    private float initialVelocityRangeX = 30f;
    private float initialVelocityRangeY = 30f;

    private float initialRadiusMin = 0.1f;
    private float initialRadiusMax = 0.7f;

    private float randomVelocityProbability = 0.0005f;

    public Rigidbody2D rb;

    void Start()
    {
        if (AntiParticle.AntiParticles == null)
        {
            AntiParticle.AntiParticles = new List<AntiParticle>();
        }

        AntiParticle.AntiParticles.Add(this);

        this.rb = this.GetComponent<Rigidbody2D>();

        this.ComputeRandomVelocity(true);

        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(50f, Screen.width / 1.5f), Random.Range(50f, Screen.height / 1.5f), Camera.main.farClipPlane / 2));

        float r = Random.Range(this.initialRadiusMin, this.initialRadiusMax);
        this.transform.localScale = new Vector3(r, r, 1);
    }

    void OnDestroy()
    {
        AntiParticle.AntiParticles.Remove(this);
    }

    void ComputeRandomVelocity(bool initial = false)
    {
        float x = Random.Range(-this.initialVelocityRangeX, this.initialVelocityRangeX);
        float y = Random.Range(-this.initialVelocityRangeY, this.initialVelocityRangeY);
        if (initial)
        {
            this.rb.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
            return;
        }
        this.rb.AddForce(new Vector2(x / 1.5f, y / 1.5f));
    }

    void FixedUpdate()
    {
        foreach (Particle p in Particle.Particles)
        {
            Vector2 distance = p.transform.position - this.transform.position;
            float distanceScalar = distance.magnitude;
            float forceScalarToParticle = AntiParticle.G * (p.rb.mass * this.rb.mass * 10f) / Mathf.Pow(distanceScalar, 2);
            float forceScalarToAntiParticle = AntiParticle.G * (p.rb.mass * this.rb.mass / 10f) / Mathf.Pow(distanceScalar, 2);
            Vector2 forceToParticle = distance.normalized * forceScalarToParticle * Time.deltaTime;
            Vector2 forceToAntiParticle = distance.normalized * forceScalarToAntiParticle * Time.deltaTime;
            p.rb.AddForce(forceToParticle);
            this.rb.AddForce(forceToAntiParticle);
        }
        foreach (AntiParticle ap in AntiParticle.AntiParticles)
        {
            if (ap.Equals(this))
            {
                continue;
            }
            Vector2 distance = ap.transform.position - this.transform.position;
            float distanceScalar = distance.magnitude;
            float forceScalarToAntiParticle = AntiParticle.G * (ap.rb.mass * this.rb.mass) / Mathf.Pow(distanceScalar, 2);
            Vector2 forceToAntiParticle = distance.normalized * forceScalarToAntiParticle * Time.deltaTime;
            this.rb.AddForce(forceToAntiParticle);
        }

        if (Random.Range(0f, 1f) < this.randomVelocityProbability)
        {
            this.ComputeRandomVelocity();
        }
    }
}
