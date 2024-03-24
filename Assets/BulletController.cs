using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            rb.velocity = direction * speed;


            Vector3 rotation = Quaternion.LookRotation(Vector3.forward, direction).eulerAngles;
            transform.rotation = Quaternion.Euler(0, 0, rotation.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}




