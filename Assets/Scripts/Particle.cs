using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public static List<Particle> Particles;

    private static float G = 60;

    private float initialVelocityRangeX = 3;
    private float initialVelocityRangeY = 3;

    private float initialRadiusMin = 0.5f;
    private float initialRadiusMax = 1f;

    public Rigidbody2D rb;
    
    void Start()
    {
        if (Particle.Particles == null)
        {
            Particle.Particles = new List<Particle>();
        }

        Particle.Particles.Add(this);

        this.rb = this.GetComponent<Rigidbody2D>();
        
        float x = Random.Range(-this.initialVelocityRangeX, this.initialVelocityRangeX);
        float y = Random.Range(-this.initialVelocityRangeY, this.initialVelocityRangeY);
        
        this.rb.AddForce(new Vector2(x, y), ForceMode2D.Impulse);

        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(50, Screen.width / 1.5f), Random.Range(50, Screen.height / 1.5f), Camera.main.farClipPlane / 2));
        
        float r = Random.Range(this.initialRadiusMin, this.initialRadiusMax);
        this.transform.localScale = new Vector3(r, r, 1);
    }

    void OnDestroy()
    {
        Particle.Particles.Remove(this);
    }

    void FixedUpdate()
    {
        foreach (Particle p in Particle.Particles)
        {
            if (p.Equals(this))
            {
                continue;
            }
            Vector2 distance = p.transform.position - this.transform.position;
            float distanceScalar = distance.magnitude;
            float forceScalar = Particle.G * (p.rb.mass * this.rb.mass) / Mathf.Pow(distanceScalar, 2);
            Vector2 force = distance.normalized * forceScalar * Time.deltaTime;
            this.rb.AddForce(force);
        }
    }
}
