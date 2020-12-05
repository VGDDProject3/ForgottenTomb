using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWall : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;
    #endregion

    void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touch wall back");
            
            foreach(Rigidbody2D rb in GetComponentsInChildren<Rigidbody2D>())
            {
                rb.isKinematic = false;
            }
            //Destroy(this.gameObject);
        }
     }
}
