using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    public float fireRate = 1f;

    private PlayerController controller;
    private float nextFire;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire && !controller.isClimbing)
        {
            nextFire = Time.time + fireRate;
            controller.isAttacking = true;
        }
    }

    public void Attack()
    {
        AudioManager.Instance.PlaySFXSound("arrowShot");
        Instantiate(arrow, arrowSpawnPoint.position, transform.rotation);
        controller.isAttacking = false;
    }
}
