﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos, startposy;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private float parallaxEffect;

    [SerializeField]
    private bool changeY = true;


    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        startposy = transform.position.y; //added
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        if (changeY) {
            float disty = (cam.transform.position.y * parallaxEffect);
            transform.position = new Vector3(startpos + dist, startposy + disty, transform.position.z);
        } else {
            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        }

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
