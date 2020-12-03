using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private bool touched = false;
    [SerializeField]
    private float countdown = 30;

    void Update() {
        if (countdown == 0 && touched) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
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
}
