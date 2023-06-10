using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    int ID;

    public float mass;
    public Vector2 position;
    public Vector2 velocity;

    public bool anchored;
    private Rigidbody2D rb;

    private void Start()
    {
        ID = ParticleSystem.Instance.RegisterParticle(this);

        rb = GetComponent<Rigidbody2D>();
    }

    public void UpdateParticle()
    {
        position = transform.position;

        if(!anchored)
        {
            Vector2 A = Vector2.zero;
            foreach(Particle particle in ParticleSystem.Instance.particles)
            {
                if (particle == this) continue;

                Vector2 F = ParticleSystem.Instance.computeForce(this, particle);

                Debug.DrawRay(position, F);

                A += F / mass;
                Debug.DrawRay(position, A, Color.red);
            }

            velocity += A * Time.deltaTime;
            Debug.DrawRay(position, velocity, Color.yellow);

            rb.velocity = velocity;
        }
    }

    private void FixedUpdate()
    {
        UpdateParticle();
    }
}
