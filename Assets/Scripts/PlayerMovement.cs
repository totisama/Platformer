using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float jumpForce = 14f;
    [SerializeField] LayerMask jumpableGround;

    float horizontalMovement;
    bool jump;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D coll;

    private enum Animations {
        Idle,
        Running,
        Jumping,
        Falling
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
        if (horizontalMovement < 0f)
        {
            currentAnimation = Animations.Running;
            spriteRenderer.flipX = true;

        }
        else if (horizontalMovement > 0f)
        {
            currentAnimation = Animations.Running;
            spriteRenderer.flipX = false;
        }
        else
        {
            currentAnimation = Animations.Idle;
        }

        //Jumping conditions
        if (rigidBody.velocity.y > 0.1f)
        {
            currentAnimation = Animations.Jumping;
        }
        else if (rigidBody.velocity.y < -0.1f)
        {
            currentAnimation = Animations.Falling;

        }

        animator.SetInteger("currentAnimation", (int) currentAnimation);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, jumpableGround);
    }
}
