using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcController : MonoBehaviour
{
    public float speed = 1f;

    [SerializeField]
    protected Transform movementTarget { get; set; }

    public bool isMoving = true;

    [SerializeField]
    protected int health;

    [SerializeField]
    public string npcName { get; set; }

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

    public void Start()
    {



    }

    void Update()
    {

    }

}
