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
    private bool cancelMomentumBeforeDash;

    [SerializeField]
    private bool hasHighVelocityEffect;

    [SerializeField]
    private float minVelocityForEffect;

    [SerializeField]
    private float jumpCoyoteTime;

    [SerializeField]
    private float wallJumpCoyoteTime;

    [SerializeField]
    private GameObject objectPoolPrefab;

    [SerializeField]
    private GameObject jumpEffect;

    [SerializeField]
    private GameObject afterimage;

    [SerializeField]
    private GameObject landingEffect;

    [SerializeField]
    private GameObject runEffect;

    [SerializeField]
    private GameObject dashEffect;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private GameObject wallSlideEffect;

    [SerializeField]
    private PlayerSoundSources playerSoundSources;

    [SerializeField]
    private float walkSoundPeriod;
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

    private bool hasAfterimages = false;
    private bool isTouchingEnvironmentWall = false;

    private Side? lastWallJumpedSide;

    private float lastTimeOnWall = -1;
    private float lastTimeGrounded = -1;

    private bool canWallJump = false;
    private bool canGroundJump = false;

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }
    public bool IsTouchingEnvironmentWall { get => isTouchingEnvironmentWall; set => isTouchingEnvironmentWall = value; }
    private ObjectPool jumpEffectObjectPool, afterimageObjectPool, landingEffectObjectPool, runEffectObjectPool, dashEffectObjectPool, deathEffectObjectPool;
    private ParticleEffectBehavior wallSlideBehavior;
    private bool wasGrounded = false;

    private float lastTimeWalkSound = 0;

    private bool canMove = true;
    public bool CanMove { get => canMove; set => canMove = value; }
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

        jumpEffectObjectPool = Instantiate(objectPoolPrefab, Vector3.zero, Quaternion.identity).GetComponent<ObjectPool>();
        jumpEffectObjectPool.CreateObjectPoolWithPrefab(jumpEffect, startingAirJumps + 1);

        afterimageObjectPool = Instantiate(objectPoolPrefab, Vector3.zero, Quaternion.identity).GetComponent<ObjectPool>();
        afterimageObjectPool.CreateObjectPoolWithPrefab(afterimage);

        landingEffectObjectPool = Instantiate(objectPoolPrefab, Vector3.zero, Quaternion.identity).GetComponent<ObjectPool>();
        landingEffectObjectPool.CreateObjectPoolWithPrefab(landingEffect, 3);

        runEffectObjectPool = Instantiate(objectPoolPrefab, Vector3.zero, Quaternion.identity).GetComponent<ObjectPool>();
        runEffectObjectPool.CreateObjectPoolWithPrefab(runEffect, 20);

        dashEffectObjectPool = Instantiate(objectPoolPrefab, Vector3.zero, Quaternion.identity).GetComponent<ObjectPool>();
        dashEffectObjectPool.CreateObjectPoolWithPrefab(dashEffect, startingDashes + 1);

        deathEffectObjectPool = Instantiate(objectPoolPrefab, Vector3.zero, Quaternion.identity).GetComponent<ObjectPool>();
        deathEffectObjectPool.CreateObjectPoolWithPrefab(deathEffect, 1);

        wallSlideBehavior = Instantiate(wallSlideEffect, this.transform).GetComponent<ParticleEffectBehavior>();
        wallSlideBehavior.ToggleParticleEmission(false);

        TouchedGroundReset();
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        if (!canMove) {
            return;
        }
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        anim.SetFloat("xvelocity", Mathf.Abs(movementX));
        anim.SetBool("grounded", IsGrounded);

        if (isGrounded)
        {
            lastTimeGrounded = Time.time;
            canGroundJump = true;
        }
        else
        {
            canGroundJump = (Time.time - jumpCoyoteTime) < lastTimeGrounded;
        }

        if (isTouchingEnvironmentWall
            && !canGroundJump
            && ((isFacingRight && lastWallJumpedSide != Side.Right) ||
            !isFacingRight && lastWallJumpedSide != Side.Left))
        {
            lastTimeOnWall = Time.time;
            canWallJump = true;

            anim.SetBool("wallsliding", true);
            wallSlideBehavior.ToggleParticleEmission(true);
        }
        else
        {
            canWallJump = (Time.time - wallJumpCoyoteTime) < lastTimeOnWall;

            anim.SetBool("wallsliding", false);
            wallSlideBehavior.ToggleParticleEmission(false);
        }





        if (isDashing)
        {
            return;
        }
        else if (hasHighVelocityEffect)
        {
            hasAfterimages = (rb.velocity.magnitude > minVelocityForEffect);
        }

        if (Input.GetButtonDown("Jump") && canWallJump)
        {
            WallJump();
        }
        else if (Input.GetButtonDown("Jump") && !canWallJump && (canGroundJump || numAirJumps >= 1))
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
        if (hasAfterimages)
        {
            afterimageObjectPool.GetFromPool();
        }
        if (isDashing)
        {
            return;
        }
        Move();

            // the landing effect
        if (isGrounded && !wasGrounded)
        {
            GameObject tempLandingEffect = landingEffectObjectPool.GetFromPool();
            tempLandingEffect.transform.position = this.transform.position;
        }
        wasGrounded = isGrounded;
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

        if (isGrounded && Mathf.Abs(movementX) > 0)
        {
            float currTime = Time.fixedTime;
            if (currTime - lastTimeWalkSound >= walkSoundPeriod)
            {
                Debug.Log(currTime);
                playerSoundSources.PlayWalkSound();
                lastTimeWalkSound = currTime;
            }
        }

        if (Mathf.Abs(rb.velocity.x) < maxPlayerInputMoveSpeed)
        {
            rb.AddForce(new Vector2(moveForce * movementX, 0));
        }
        else if (IsGrounded)
        {
            if (Random.Range(1, 3) == 1)
            {
                GameObject tempRunEffect = runEffectObjectPool.GetFromPool();
                tempRunEffect.transform.position = this.transform.position;
                tempRunEffect.transform.localScale = this.transform.localScale;
            }
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
        playerSoundSources.PlayWallJumpSound();
        // creating and destroying the jump effect
        GameObject tempJumpEffect = jumpEffectObjectPool.GetFromPool();
        tempJumpEffect.transform.position = this.transform.position;
        anim.SetTrigger("jump");
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(force, Mathf.Abs(force)));
    }

    private void Jump()
    {
        if (!isGrounded)
        {
            numAirJumps--;
            playerSoundSources.PlayAirJumpSound(maxAirJumps - numAirJumps);
        }
        else
        {
            isGrounded = false;
            playerSoundSources.PlayJumpSound();
        }

        // creating and destroying the jump effect
        GameObject tempJumpEffect = jumpEffectObjectPool.GetFromPool();
        tempJumpEffect.transform.position = this.transform.position;
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
            // just 'cuz I implemented both – if "isMouse" is true, the dash angle is calculated from the mouse position;
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
        GameObject tempDashEffect = dashEffectObjectPool.GetFromPool();
        tempDashEffect.transform.position = this.transform.position;
        tempDashEffect.transform.LookAt(( (Vector2) this.transform.position) + dashDirection);

        float originalGravityScale = rb.gravityScale;

        //rb.velocity = Vector2.zero;
        isDashing = true;

        if (cancelMomentumBeforeDash)
        {
            rb.velocity = Vector2.zero;
        }
        hasAfterimages = true;
        rb.gravityScale = 0;

        rb.AddForce(dashDirection);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravityScale;
        isDashing = false;

        yield return new WaitForSeconds(0.1f);

        hasAfterimages = false;

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
        GameObject tempDeathEffect = deathEffectObjectPool.GetFromPool();
        tempDeathEffect.transform.position = this.transform.position;
        this.GetComponent<Grapple>().DetachGrapple();
        movementX = 0;
        movementY = 0;
        rb.velocity = Vector2.zero;

    }
    #endregion

}