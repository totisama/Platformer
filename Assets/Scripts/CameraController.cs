using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private void Update()
    {
        Vector3 position = playerTransform.position;

        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
