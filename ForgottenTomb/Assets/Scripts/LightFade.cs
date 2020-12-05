using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightFade : MonoBehaviour
{
    [SerializeField]
    private float lifetime;

    [SerializeField]
    private AnimationCurve curve = AnimationCurve.Linear(1, 1, 0, 0);

    [SerializeField]
    private bool hasLifetime = true;

    private float creationTime = -1;

    private float startingIntensity;

    private Light2D light;

    private void Start()
    {
        light = GetComponent<Light2D>();
        startingIntensity = light.intensity;
    }

    private void OnEnable()
    {
        creationTime = Time.time;
    }

    void Update()
    {
        if ((creationTime + lifetime <= Time.time) && hasLifetime && (creationTime != -1))
        {
            light.intensity = 0;
            creationTime = -1;
        }
        else
        {
            light.intensity = startingIntensity * curve.Evaluate((Time.time - creationTime) / lifetime);
        }
    }
}
