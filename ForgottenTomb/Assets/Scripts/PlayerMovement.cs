﻿using System.Collections;
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

    [SerializeField]
    private float dashForce;

    [SerializeField]
    private int maxDashes;

    [SerializeField]
    private int startingDashes;

    [SerializeField]
    private float dashTime; 
    #endregion

    #region Cached Components
    private Rigidbody2D rb;
    #endregion


    #region Private Variables
    private float movementX;
    private int numAirJumps;

    private int numDashes;

    private bool isGrounded = false;

    private bool isFacingRight = true;

    private bool isDashing = false;

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }
    #endregion

    #region On Start Functions
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TouchedGroundReset();
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        if (isDashing)
        {
            return;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || numAirJumps >= 1))
        {
            Jump();
        }

        if (Input.GetButtonDown("Dash") && !isGrounded && numDashes >= 1)
        {
            StartDash();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Move();
    }

    #endregion

    #region Movement Functions
    private void Move()
    {
        if (movementX * rb.velocity.x <= 0) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        rb.AddForce(new Vector2(moveForce * movementX, 0));

        if (rb.velocity.x > maxMoveSpeed) {
            rb.velocity = new Vector2(maxMoveSpeed, rb.velocity.y);
        } else if (rb.velocity.x < -maxMoveSpeed) {
            rb.velocity = new Vector2(-maxMoveSpeed, rb.velocity.y);
        }

        if ((movementX > 0 && !IsFacingRight) || (movementX < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (!isGrounded)
        {
            numAirJumps--;
        }
        else
        {
            isGrounded = false;
        }
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce));
    }

    private void StartDash()
    {
        numDashes--;
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        float originalGravityScale = rb.gravityScale;
        rb.velocity = Vector2.zero;
        isDashing = true;
        rb.gravityScale = 0;

        if (isFacingRight)
        {
            rb.AddForce(new Vector2(dashForce, 0));
        }
        else
        {
            rb.AddForce(new Vector2(-dashForce, 0));
        }
        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravityScale;
        isDashing = false;
    }

    private void Flip()
    {
        IsFacingRight = !isFacingRight;

        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    #endregion

    #region Public functions
    public void TouchedGroundReset()
    {
        numAirJumps = startingAirJumps;
        numDashes = startingDashes;
    }

    public void IncreaseNumAirJumps(int num)
    {
        numAirJumps = Mathf.Min(numAirJumps + num, maxAirJumps);
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }
    #endregion
}