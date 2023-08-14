using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    [SerializeField] private int distance = 4;
    [SerializeField] private float velocity = 1f;
    [SerializeField] private GameObject objectToMove;

    private float topLimit;
    private float bottomLimit;
    private float t = 0.0f;
    private void Start()
    {
        topLimit = objectToMove.transform.position.y;
        bottomLimit = topLimit - distance;
    }
    
    void Update()
    {
        Vector3 currentPosition = objectToMove.transform.position;
        objectToMove.transform.position = new Vector3(currentPosition.x, Mathf.Lerp(topLimit, bottomLimit, t), currentPosition.z);

        t += velocity * Time.deltaTime;

        if (t > 1.0f)
        {
            float temp = topLimit;
            topLimit = bottomLimit;
            bottomLimit = temp;
            t = 0.0f;
        }
    }
}
