using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float health;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private PlayerController playerController;

    public float Health
    {
        get { return health; }
        private set { health = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();

        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("death");
    }
}
