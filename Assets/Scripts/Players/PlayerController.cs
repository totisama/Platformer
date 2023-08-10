using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    public bool canMove = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;

    [Header("Ground Check")]
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask jumpableGround;

    private float horizontalMovement;
    private bool jump;
    [HideInInspector]
    public bool isAttacking;

    Rigidbody2D rigidBody;
    Animator animator;
    BoxCollider2D coll;

    private enum Animations {
        Idle,
        Run,
        Jump,
        Fall,
        Attack
    };

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !isAttacking)
        {
            jump = true;
        }

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        MovePlayer();

        if (jump)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        if (!isAttacking || (isAttacking && !IsGrounded()))
        {
            rigidBody.velocity = new Vector2(horizontalMovement * moveSpeed, rigidBody.velocity.y);
        }
        else if ((horizontalMovement < 0f || horizontalMovement > 0f) && isAttacking)
        { 
            // Set x velocity to 0 when the player is moving and attacking
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }
    }
    
    private void Jump()
    {
        AudioManager.Instance.PlaySFXSound("jump");
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        jump = false;
    }

    private void UpdateAnimation()
    {
        Animations currentAnimation;

        //Running conditions
        if (horizontalMovement < 0f || horizontalMovement > 0f)
        {
            FlipPlayer();
            currentAnimation = Animations.Run;
        }
        else
        {
            currentAnimation = Animations.Idle;
        }

        //Jumping conditions
        if (rigidBody.velocity.y > 0.1f)
        {
            currentAnimation = Animations.Jump;
        }
        else if (rigidBody.velocity.y < -0.1f)
        {
            currentAnimation = Animations.Fall;

        }

        //Attack conditions
        if (isAttacking)
        {
            currentAnimation = Animations.Attack;
        }

        if(animator.GetInteger("currentAnimation") != (int) currentAnimation)
        {
            animator.SetInteger("currentAnimation", (int) currentAnimation);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, jumpableGround);
    }

    private void FlipPlayer()
    {
        if (horizontalMovement < 0f && canMove)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalMovement > 0f && canMove)
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
