using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float force;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
