using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] private LayerMask whatDestroysBullet;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float bulletDamage = 15f;

    [Header("Normal Stats")]
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float bulletGravity = 3f;

    [Header("Physics Stats")]
    [SerializeField] private float physicsBulletSpeed = 15f;

    private Rigidbody2D rb;
    private enum BulletType
    {
        normal,
        physics
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        InitializeBullet();
        SetRBStats();
        DestroyBullet();
    }

    private void FixedUpdate()
    {
        if (bulletType == BulletType.physics)
        {
            transform.right = rb.velocity;
        }
    }

    private void InitializeBullet()
    {
        if (bulletType == BulletType.normal)
        {
            SetStraightVelocity();
        }
        else if (bulletType == BulletType.physics)
        {
            SetPhysicsVelocity();
        }
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * normalBulletSpeed;
    }
    
    private void SetPhysicsVelocity()
    {
        rb.velocity = transform.right * physicsBulletSpeed;
    }
    
    private void SetRBStats()
    {
        if (bulletType == BulletType.normal)
        {
            rb.gravityScale = 0f;
        }
        else if (bulletType == BulletType.physics)
        {
            rb.gravityScale = bulletGravity;
        }
    }
    
    private void DestroyBullet()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Is the collision is on the specific layerMask
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();

            if (iDamageable != null)
            {
                //Damage enemy
                iDamageable.TakeDamage(bulletDamage, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
