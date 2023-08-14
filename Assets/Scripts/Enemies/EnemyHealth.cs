using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float health = 100f;

    [Header("Canvas")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Canvas canvas;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Slider properties
        healthSlider.value = health;
        healthSlider.maxValue = health;
    }

    public void TakeDamage(float damage, Vector2 damageDirection)
    {
        AudioManager.Instance.PlaySFXSound("hit");

        health -= damage;
        healthSlider.value = health;

        if (health <= 0)
        {
            rb.simulated = false;
            canvas.enabled = false;
            animator.SetTrigger("death");
        }
    }

    // Function called in an animation event
    private void Die()
    {
        Destroy(gameObject);
    }
}
