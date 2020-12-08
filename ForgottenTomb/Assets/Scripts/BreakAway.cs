using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAway : MonoBehaviour, Resettable
{
    private void Start()
    {
        FindObjectOfType<CheckpointManager>().AddToThingsToReset(this);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Boulder")
        {
            //Debug.Log("game object destroyed");
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Boulder")
        {
            //Debug.Log("game object destroyed");
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }

}
