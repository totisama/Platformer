using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 7f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 14f;

    [Header("Ground Check")]
    [SerializeField] float extraHeight = 0.25f;
    [SerializeField] LayerMask jumpableGround;

    float horizontalMovement;
    bool jump;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D coll;

    private enum Animations {
        Idle,
        Run,
        Jump,
        Fall
    };

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jump = true;
        }

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (jump)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        rigidBody.velocity = new Vector2(horizontalMovement * moveSpeed, rigidBody.velocity.y);
    }
    
    private void Jump()
    {
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

        animator.SetInteger("currentAnimation", (int) currentAnimation);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, jumpableGround);
    }

    private void FlipPlayer()
    {
        if (horizontalMovement < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalMovement > 0f)
        {
            spriteRenderer.flipX = false;
        }
    }
}
