using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour, Resettable
{
    [SerializeField]
    private bool resetOnPlayerDeath = true;
    [SerializeField]
    private float initialCountdown = 30;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float countdown;
    private Rigidbody2D rb; 

    private bool touched = false;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        if (resetOnPlayerDeath)
        {
            FindObjectOfType<CheckpointManager>().AddToThingsToReset(this);
        }
        rb = GetComponent<Rigidbody2D>();
        countdown = initialCountdown;
    }
    void Update() {
        if (countdown <= 0 && touched) {
            
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
        } 
        if (touched) {
            countdown -= 1;
        }

    }
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Player"))
        {
            Debug.Log("touch fall block");
            touched = true;
        }
    }

    public void Reset()
    {
        touched = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        countdown = initialCountdown;
    }
}
