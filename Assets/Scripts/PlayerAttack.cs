using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    public float fireRate = 1f;

    private PlayerController controller;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void Attack()
    {
        Instantiate(arrow, arrowSpawnPoint.position, transform.rotation);
        controller.isAttacking = false;
    }
}
