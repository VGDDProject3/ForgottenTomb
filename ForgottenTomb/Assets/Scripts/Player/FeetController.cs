using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private float forgiveTime;
    
    [SerializeField]
    private PlayerMovement playerMovement;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Environment"))
        {
            playerMovement.IsGrounded = true;
            playerMovement.TouchedGroundReset();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Environment") && Mathf.Abs(playerMovement.getVelocity().y) < 0.01f)
        {
            playerMovement.IsGrounded = true;
            playerMovement.TouchedGroundReset();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Environment"))
        {
            StartCoroutine(SetNotGrounded());
        }
    }

    private IEnumerator SetNotGrounded()
    {
        // Lets the player make jumps they slightly shouldn't
        yield return new WaitForSeconds(forgiveTime);
        playerMovement.IsGrounded = false;
    }
}