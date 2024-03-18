using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcController : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]
    public Transform movementTarget { get; private set; }
    public bool isMoving = true;

    
    public void MoveToTransform(Transform target)
    {
        if(movementTarget != target)
        {
            isMoving = true;
            movementTarget = target;
        }

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );

        if (transform.position.x == target.position.x)
        {
            isMoving = false;
        }

    }

    private void OnMouseDown()
    {
        Debug.Log("active");
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTransform(movementTarget); 
        }
    }
}
