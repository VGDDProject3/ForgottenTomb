using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;
    #endregion
    public void ObtainKey() 
    {
        playerMovement.Keys = playerMovement.Keys + 1;

    }

    void OnTriggerEnter2d(Collider2D collision)
     {
         if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("touch key");
            ObtainKey();
            Destroy(this.gameObject);
        }
     }
}
