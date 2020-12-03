using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAway : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Boulder")
        {
            Debug.Log("game object destroyed");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Boulder")
        {
            Debug.Log("game object destroyed");
            Destroy(gameObject);
        }
    }
}
