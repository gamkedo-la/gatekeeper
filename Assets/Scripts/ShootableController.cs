using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void Hit()
    {
        rb.velocity = new Vector2(rb.velocity.x, 10.0f);
    }
}
