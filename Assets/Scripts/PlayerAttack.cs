using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSpawnPoint;

    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void Attack()
    {
        Instantiate(arrow, arrowSpawnPoint.position, transform.rotation);
        controller.isAttacking = false;
    }
}
