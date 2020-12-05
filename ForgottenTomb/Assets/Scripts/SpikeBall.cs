using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class SpikeBall : MonoBehaviour 
 {
 
     private float RotateSpeed = 5f;
     private float Radius = 5f;
 
     private Vector2 _centre;
     private float _angle;

     private LineRenderer chain;


     void Start()
     {
        chain = GetComponent<LineRenderer>();
         _centre = transform.position;
        chain.SetPosition(0, _centre);
     }
 
     void Update()
     {
 
         _angle += RotateSpeed * Time.deltaTime;
 
         var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
         transform.position = _centre + offset;

        chain.SetPosition(1, transform.position);
     }

     void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerMisc>().Die();
        }
    }
 
 }
