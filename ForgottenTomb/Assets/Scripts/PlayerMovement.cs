using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    public enum Side
    {
        Left,
        Right
    }
    #endregion

    #region Editor Variables
    [SerializeField]
    private float moveForce;

    [SerializeField]
    private float maxMoveSpeed;

    [SerializeField]
    private float maxPlayerInputMoveSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float wallJumpForce;

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

    [SerializeField]
    [Range(0.1f, 1)]
    private float xVelDecayRate;

    [SerializeField]
    private float minimumXVelocity;

    [SerializeField]
    private bool useMousePosForDash;

    [SerializeField]
    private GameObject jumpEffect;

    [SerializeField]
    private GameObject dashAfterImage;

    #endregion

    #region Cached Components
    private Rigidbody2D rb;

    private Animator anim;
    #endregion


    #region Private Variables
    private float movementX;
    private float movementY;
    private Vector2 mousePos;
    private int numAirJumps;

        // the player can only influence part of the character's momentum, with its own cap.
    private float playerInputXVel = 0;

    private int numDashes;

    private bool isGrounded = false;

    private bool isFacingRight = true;

    private bool isDashing = false;

    private bool isTouchingEnvironmentWall = false;

    private Side? lastWallJumpedSide;

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }
    public bool IsTouchingEnvironmentWall { get => isTouchingEnvironmentWall; set => isTouchingEnvironmentWall = value; }
    #endregion

    #region Collectible variables
    private int keys = 0;

    public int Keys { get => keys; set => keys = value; }

    private int coins = 0;

    public int Coins { get => coins; set => coins = value; }
    #endregion

    #region On Start Functions
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        TouchedGroundReset();
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        anim.SetFloat("xvelocity", Mathf.Abs(movementX));
        anim.SetBool("grounded", IsGrounded);

        if (isDashing)
        {
            return;
        }

        if (Input.GetButtonDown("Jump") && isTouchingEnvironmentWall && ((isFacingRight && lastWallJumpedSide != Side.Right) ||
            !isFacingRight && lastWallJumpedSide != Side.Left))
        {
            WallJump();
        }
        else if (Input.GetButtonDown("Jump") && (isGrounded || numAirJumps >= 1))
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
        if (movementX * rb.velocity.x <= 0)
        {
            float newXVel = rb.velocity.x * (1 - xVelDecayRate);
            if (Mathf.Abs(newXVel) < minimumXVelocity)
            {
                newXVel = 0;
            }
            rb.velocity = new Vector2(newXVel, rb.velocity.y);
        }

        if (Mathf.Abs(rb.velocity.x) < maxPlayerInputMoveSpeed)
        {
            rb.AddForce(new Vector2(moveForce * movementX, 0));
        }
        

        
            // capping max move speed horizontally
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

    private void WallJump()
    {
        float force;
        if (isFacingRight)
        {
            force = -wallJumpForce;
            lastWallJumpedSide = Side.Right;
        }
        else
        {
            force = wallJumpForce;
            lastWallJumpedSide = Side.Left;
        }
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(force, Mathf.Abs(force)));
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

            // creating and destroying the jump effect
        Destroy(Instantiate(jumpEffect, this.transform.position, Quaternion.identity), 0.5f);
        anim.SetTrigger("jump");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce));
    }

    private void StartDash()
    {
        numDashes--;
        //rb.AddForce();//, ForceMode2D.Impulse);
        StartCoroutine(Dash(CreateDashVector(useMousePosForDash) * dashForce));
    }

    private Vector2 CreateDashVector(bool isMouse)
    {
            // just 'cuz I implemented both – if "isMouse" is true, the dash angle is calculated from the mouse position;
        if (isMouse)
        {
            
            Vector2 startPos = this.transform.position;
            return new Vector2(mousePos.x - startPos.x, mousePos.y - startPos.y).normalized;
        }
            // otherwise it's from the wasd inputs
        else
        {
            return new Vector2(movementX, movementY).normalized;
        }
    }

    private IEnumerator Dash(Vector2 dashDirection)
    {
        float originalGravityScale = rb.gravityScale;
        //rb.velocity = Vector2.zero;
        isDashing = true;
        rb.gravityScale = 0;

        rb.AddForce(dashDirection);

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
        lastWallJumpedSide = null;
    }

    public void IncreaseNumAirJumps(int num)
    {
        numAirJumps = Mathf.Min(numAirJumps + num, maxAirJumps);
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }

    public void Respawn()
    {
        movementX = 0;
        movementY = 0;
        rb.velocity = Vector2.zero;
    }
    #endregion

}