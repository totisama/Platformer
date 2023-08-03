using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SpiderEnemy : MonoBehaviour, IDamageable
{
    [Header("General")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Behavior")]
    [SerializeField] private float seekDistance = 7f;
    [SerializeField] private float attackDistance = 2f;

    [Header("Player")]
    [SerializeField] private PlayerHealth playerHealth;

    private Vector2 idlePosition;
    private Animator animator;

    private enum Animations
    {
        Idle,
        Run,
        Attack
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
        idlePosition = transform.position;
    }

    private void Update()
    {
        float sqrDistanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        Animations newAnimation = Animations.Run;

        if (sqrDistanceToPlayer <= attackDistance)
        {
            Attack();
            newAnimation = Animations.Attack;
        }
        else if(sqrDistanceToPlayer <= seekDistance)
        {
            FlipScale(playerTransform.position);
            SeekPlayer();
        }
        else
        {
            if (Vector2.Distance(transform.position, idlePosition) <= 0.5f)
            {
                newAnimation = Animations.Idle;
                FlipScale(playerTransform.position);
            }
            else
            {
                FlipScale(idlePosition);
                BackToIdlePosition();
            }
        }
        
        UpdateAnimation(newAnimation);
    }

    private void FlipScale(Vector3 target)
    {
        Vector3 scale = transform.localScale;

        if (target.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private void BackToIdlePosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, idlePosition, moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
    }

    private void SeekPlayer()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            transform.position += moveSpeed * Time.deltaTime * Vector3.left;
        }
        else
        {
            transform.position += moveSpeed * Time.deltaTime * Vector3.right;
        }
    }

    private void UpdateAnimation(Animations newAnimation)
    {
        if (animator.GetInteger("currentAnimation") != (int)newAnimation)
        {
            animator.SetInteger("currentAnimation", (int)newAnimation);
        }
    }

    public void TakeDamage(float damage, Vector2 damageDirection)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage, transform.position);
        }
    }
}
