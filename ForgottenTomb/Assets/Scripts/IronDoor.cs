using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronDoor : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private int requiredKeys;
    #endregion
    public void OpenDoor() 
    {
        if (playerMovement.Keys == requiredKeys) {
            playerMovement.Keys = 0;
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touch door");
            OpenDoor();
        }
     }
}
