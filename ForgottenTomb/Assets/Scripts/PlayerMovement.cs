using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private float moveForce;

    [SerializeField]
    private float maxMoveSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private int maxAirJumps;

    [SerializeField]
    private int startingAirJumps;
    #endregion

    #region Cached Components
    private Rigidbody2D rb;
    #endregion


    #region Private Variables
    private float movementX;
    private float airJumps;

    private bool isGrounded = false;

    private bool facingRight = true;

    private bool isLocked = false;

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool FacingRight { get => facingRight; set => facingRight = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
    #endregion

    #region On Start Functions
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetAirJumps();
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        if (IsLocked)
        {
            return;
        }
        movementX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && (isGrounded || airJumps >= 1))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region Movement Functions
    private void Move()
    {
        if (movementX * rb.velocity.x <= 0 || isLocked) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (IsLocked)
        {
            return;
        }

        rb.AddForce(new Vector2(moveForce * movementX, 0));

        if (rb.velocity.x > maxMoveSpeed) {
            rb.velocity = new Vector2(maxMoveSpeed, rb.velocity.y);
        } else if (rb.velocity.x < -maxMoveSpeed) {
            rb.velocity = new Vector2(-maxMoveSpeed, rb.velocity.y);
        }

        if ((movementX > 0 && !FacingRight) || (movementX < 0 && FacingRight))
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (!isGrounded)
        {
            airJumps--;
        }
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce));
    }

    private void Flip()
    {
        FacingRight = !FacingRight;

        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    #endregion

    #region Public functions
    public void ResetAirJumps()
    {
        airJumps = startingAirJumps;
    }

    public void IncreaseNumAirJumps(int num)
    {
        airJumps = Mathf.Min(airJumps + num, maxAirJumps);
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }
    #endregion
}