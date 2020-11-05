using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideController : MonoBehaviour
{
    private enum Side
    {
        Left,
        Right
    }

    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Side side;
    
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Environment"))
        {
            if (side.Equals(Side.Right))
            {
                playerMovement.IsTouchingEnvironmentRight = true;
            }
            else
            {
                playerMovement.IsTouchingEnvironmentLeft = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Environment"))
        {
            if (side.Equals(Side.Right))
            {
                playerMovement.IsTouchingEnvironmentRight = false;
            }
            else
            {
                playerMovement.IsTouchingEnvironmentLeft = false;
            }
        }
    }
}
