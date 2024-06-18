using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void Hit()
    {
        rb.velocity = new Vector2(Random.RandomRange(-2f,2f), 5.0f);
        rb.AddTorque(Random.RandomRange(-10f,10f));
    }
}
