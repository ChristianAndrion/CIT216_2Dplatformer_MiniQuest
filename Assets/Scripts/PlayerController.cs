using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerInput pi;
    private Vector2 moveInput;
    private bool facingRight = true;
    private bool jumpPressed = false;
    private bool isGrounded = true;
    private bool isIdle = true;

    private float health = 100;

    
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float lowJumpMultiplier = 2f;
    [Tooltip("Air Friction")]
    public float airMultiplier = 0.25f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public Transform shadowDot;
    public LayerMask groundLayer; //Checking collisions only on ground layer
    public float groundCheckRadius = 0.25f;
    public float fallGravityScale = 2f; //Update Gravity to 200%

    [Header("Shooting")]
    public Transform firePoint;
    public GameObject bullet; //Our prefab to fire

    public float attackRate = 0.5f;
    private float nextAttackTime = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInput>();
    }

    void OnMove(InputValue movementValue)
    { 
        moveInput = movementValue.Get<Vector2>();
        Debug.Log("Player Movement: " + moveInput.x);
    }

    void OnJump(InputValue movementValue)
    {
        jumpPressed = true;
        CheckGrounded();
    }

    /*void OnAttack(InputValue attackValue)
    {
        anim.SetTrigger("isShooting");
    }*/

    // Update is called once per frame
    void Update()
    {
        //Example of moving an object via translate
        //transform.Translate(moveInput * Vector2.left * Time.deltaTime);
        if (moveInput.x < 0 && facingRight)//Moving left but facing right
        {
            Flip();
            facingRight = false;
        }
        else if (moveInput.x > 0 && !facingRight)//Moving right but facing left
        {
            Flip();
            facingRight = true;
        }

        

        //Event polling
        float isAttackHeld = pi.actions["Attack"].ReadValue<float>(); //1 means held, 0 means not held

        if (isAttackHeld > 0 && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate; //Set this to allow future attack time
            anim.SetTrigger("isShooting");

            Instantiate(bullet, firePoint.position, facingRight ? firePoint.rotation : Quaternion.Euler(0, 180, 0)); //Terniary Operator


        }
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput.x * moveSpeed; //Desired Movement Speed
        float speedDiff = targetSpeed - rb.linearVelocity.x;//How far away am I from the speed I want to be
        //float accelRate = moveSpeed;
        float accelRate = isGrounded ? moveSpeed : moveSpeed * airMultiplier; //Short hand for if else statement [if statement] ? [Truth Value] : [False Value]
        float movement = speedDiff * accelRate;//How hard to push the player

        

        rb.AddForce(Vector2.right * movement);
        if(moveInput.x < 0 || moveInput.x > 0)
        {
            anim.SetBool("is_idle", false);
        }
        else
        {
            anim.SetBool("is_idle", true);
        }


        float isJumpHeld = pi.actions["Jump"].ReadValue<float>();

        if (isJumpHeld > 0 && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }

        if(rb.linearVelocityY > 0 && isJumpHeld <= 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        anim.SetFloat("jump_velocity", rb.linearVelocityY);

        //In Class jump code
        //if (jumpPressed && isGrounded)
        //{
        //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //    jumpPressed = false;
        //    isGrounded = false;
        //}

        if (rb.linearVelocity.y < 0) // Player is Falling
            rb.gravityScale = fallGravityScale;
        else
            rb.gravityScale = 1; // Default Value for Gravity

        //Check to see if we are hitting the ground from our raycast
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 50f, groundLayer);
        if (hit)
        {
            shadowDot.position = hit.point;
            shadowDot.gameObject.SetActive(true);
        }

        Debug.DrawRay(rb.position, Vector2.down, Color.red, 1f);

        CheckGrounded();

    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;// Inverts the x
        transform.localScale = theScale;//Set game object to new scale
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        anim.SetBool("is_grounded", isGrounded);
        shadowDot.gameObject.SetActive(!isGrounded); //Only show if not grounded
    }

    void OnDrawGizmos() //For Debugging Purposes
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius); //Show the circle made by CheckGrounded()
    }

    public Vector2 GetDirection()
    {
        if (facingRight)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("boundary"))
        {
            anim.SetBool("isDead", true);
            Invoke("KillPlayer", 1f); //Reload current scene
        }
        if (collision.gameObject.CompareTag("pickup"))
        {
            health += 10;
            
        }
        if (collision.gameObject.CompareTag("console"))
        {
            Debug.Log("Entered Console");
            GameManager.instance.UnlockDoor();
        }
        if (collision.gameObject.CompareTag("win"))
        {
            GameManager.instance.GameWin();
        }
    }

    void KillPlayer()
    {
        GameManager.instance.DecreaseLives(1);
        Debug.Log("Lives: " + GameManager.instance.GetLives());
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            health -= 25;
            if (health <= 0)
            {
                pi.enabled = false;
                //pi.DeactivateInput();

                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                anim.SetBool("isDead", true);
                Invoke("KillPlayer", 1f);
            }
        }

        else if (collision.gameObject.CompareTag("console"))
        {
            GameManager.instance.UnlockDoor();
        }
    }
}
