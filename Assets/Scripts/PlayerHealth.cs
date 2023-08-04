using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float timeToMove = 1f;
    [SerializeField] private Slider healthSlider;
    private float health;

    private Animator animator;
    private PlayerController playerController;
    private TakeKnockBack takeKnockBack;

    public float Health
    {
        get { return health; }
        private set { health = value; }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        takeKnockBack = GetComponent<TakeKnockBack>();
    }

    private void Start()
    {
        health = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void TakeDamage(float damage, Vector2 damageDirection)
    {
        takeKnockBack.KnockBack(damageDirection);
        Health -= damage;
        healthSlider.value = Health;

        if (Health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(ImmobilizePlayer());
        }
    }

    private void Die()
    {
        playerController.canMove = false;
        animator.SetTrigger("death");
    }

    private IEnumerator ImmobilizePlayer()
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(timeToMove);
        playerController.canMove = true;
    }
}
