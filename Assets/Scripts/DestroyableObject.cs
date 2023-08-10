using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float hits = 3f;
    [SerializeField] private bool dropObjectsOnDestroy;
    [SerializeField] private GameObject dropObject;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage, Vector2 damageDirection)
    {
        hits -= 1f;

        if (hits <= 0)
        {
            animator.SetTrigger("destroy");

            if (dropObjectsOnDestroy)
            {

            }
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
