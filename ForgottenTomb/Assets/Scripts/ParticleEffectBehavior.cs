using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectBehavior : MonoBehaviour
{
    [SerializeField]
    private float lifetime;

    [SerializeField]
    private bool hasLifetime = true;

    private float creationTime = -1;

    private void OnEnable()
    {
        creationTime = Time.time;
    }

    private void Update()
    {
        if ((creationTime + lifetime <= Time.time) && hasLifetime && (creationTime != -1))
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
}
