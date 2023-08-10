using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    [SerializeField] private CpllectableType type;

    private Animator animator;

    private enum CpllectableType
    {
        coin
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void CollectItem()
    {
        animator.SetBool("collected", true);
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectItem();
        }
    }
}
