using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchFlicker : MonoBehaviour
{
    private Light torchLight;
    private Light2D torchLightURP;
    private Vector2 startPosition;

    [SerializeField]
    private float maxIntensity, minIntensity, maxChangePerFrame;

    [SerializeField]
    private bool usesURP = true;

    //[SerializeField]
    //private int randomVariationChance;

    //[SerializeField]
    //private float ranDist;


    private void Start()
    {
        if (usesURP)
        {
            torchLightURP = GetComponent<Light2D>();
        }
        else
        {
            torchLight = GetComponent<Light>();
        }
        
        startPosition = transform.position;
    }
    private void Update()
    {
        if (usesURP)
        {
            torchLightURP.intensity = Mathf.Min(maxIntensity, Mathf.Max(minIntensity, torchLightURP.intensity + Random.Range(-maxChangePerFrame, maxChangePerFrame)));
        }
        else
        {
            torchLight.intensity = Mathf.Min(maxIntensity, Mathf.Max(minIntensity, torchLight.intensity + Random.Range(-maxChangePerFrame, maxChangePerFrame)));
        }
        //if (Random.Range(0, randomVariationChance) == 1)
        //{
        //    transform.position = startPosition + new Vector2(Random.Range(-ranDist, ranDist), Random.Range(-ranDist, ranDist));
        //}
        //if ((Vector2) transform.position != startPosition)
        //{
        //    transform.position = Vector2.Lerp(transform.position, startPosition, Random.Range(0.5f, 1));
        //}
    }
}
