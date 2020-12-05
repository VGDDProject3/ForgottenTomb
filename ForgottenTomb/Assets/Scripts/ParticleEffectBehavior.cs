using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectBehavior : MonoBehaviour
{
    [SerializeField]
    private float lifetime;

    [SerializeField]
    private bool hasLifetime = true;

    [SerializeField]
    private bool enabledOnStart = true;

    private float creationTime = -1;

    private void OnEnable()
    {
        creationTime = Time.time;
        ToggleParticleEmission(enabledOnStart);
    }

    private void Update()
    {
        if (!hasLifetime)
        {
            return;
        }

        if ((creationTime + lifetime <= Time.time) && (creationTime != -1))
        {
            creationTime = -1;
            ObjectPool pool = GetComponentInParent<ObjectPool>();
            if (pool)
            {
                //print("lifetime ran out, adding back to pool");
                
                pool.AddToPool(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void ToggleParticleEmission(bool emit)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (emit && ps.isStopped)
        {
            ps.Play();
        }
        if (!emit && ps.isPlaying)
        {
            ps.Stop();
        }
    }
}
