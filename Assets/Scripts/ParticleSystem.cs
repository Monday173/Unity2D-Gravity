using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    public static ParticleSystem Instance;

    private int current_id;
    public List<Particle> particles = new List<Particle>();

    public float G;

    public void Awake()
    {
        Instance = this;
        G = 9.8f;
    }

    private int sign(float a)
    {
        if (a < 0) return -1;

        return 1;
    }

    public int RegisterParticle(Particle particle)
    {
        particles.Add(particle);

        int ret = current_id;
        current_id++;

        return ret;
    }

    public Vector2 computeForce(Particle a, Particle b)
    {
        float d = (a.position - b.position).magnitude;
        float F = (G * a.mass * b.mass) / d;

        Vector2 dir = (b.position - a.position).normalized;

        return dir * F;
    }

    public void UnregisterParticle(int particleID)
    {
        particles.RemoveAt(particleID);
    }
}
