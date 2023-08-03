using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float timeToMove = 1f;
    private float health;

    private Animator animator;
    private PlayerController playerController;
    private TakeKnockBack takeKnockBack;

    public float Health
    {
        get { return health; }
        private set { health = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        takeKnockBack = GetComponent<TakeKnockBack>();

        health = maxHealth;
    }

    public void TakeDamage(float damage, Vector2 damageDirection)
    {
        takeKnockBack.KnockBack(damageDirection);
        Health -= damage;

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
