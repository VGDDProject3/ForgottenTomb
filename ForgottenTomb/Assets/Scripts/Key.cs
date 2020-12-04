using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private GameObject collectionEffect;
    #endregion
    public void ObtainKey() 
    {
        playerMovement.Keys = playerMovement.Keys + 1;

    }

    void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touch key");
            ObtainKey();
            if (collectionEffect != null)
            {
                Instantiate(collectionEffect, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
     }
}
