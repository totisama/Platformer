using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeKnockBack : MonoBehaviour
{
    [SerializeField] private Vector2 knockBackForce;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void KnockBack(Vector2 direction)
    {
        rb.velocity = new Vector2(knockBackForce.x * direction.normalized.x, knockBackForce.y);
    }
}
