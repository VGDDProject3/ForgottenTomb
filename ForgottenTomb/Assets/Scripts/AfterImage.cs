using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private Transform playerTransform;

    private SpriteRenderer renderer;
    private SpriteRenderer playerRenderer;

    private Color currentColor;

    [SerializeField]
    private float activeTime = 0.1f;
    private float startTime;
    private float currentAlpha;

    [SerializeField]
    private float startingAlpha = 0.7f;

    [SerializeField]
    private float alphaMultiplier = 0.5f;

    //private void Start()
    //{
    //    this.gameObject.SetActive(false);
    //}

    private void OnEnable()
    {
        renderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerRenderer = playerTransform.GetComponent<SpriteRenderer>();

        currentAlpha = startingAlpha;
        renderer.sprite = playerRenderer.sprite;
        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation;
        renderer.flipX = playerTransform.localScale.x < 0;
        startTime = Time.time;
    }

    private void Update()
    {
        currentAlpha = currentAlpha * alphaMultiplier;
        currentColor = new Color(1f, 1f, 1f, currentAlpha);
        renderer.color = currentColor;
        if (Time.time >= (startTime + activeTime))
        {
            GetComponentInParent<ObjectPool>().AddToPool(this.gameObject);
            //this.gameObject.SetActive(false);
        }
    }
}
