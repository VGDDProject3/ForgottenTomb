using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideController : MonoBehaviour
{

    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Environment"))
        {
            playerMovement.IsTouchingEnvironmentWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Environment"))
        {
            playerMovement.IsTouchingEnvironmentWall = false;
        }
    }
}
