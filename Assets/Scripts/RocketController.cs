using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D rigidbody2D;

    void Start()
    {
        
    }


    void Update()
    {
        rigidbody2D.transform.position = Vector3.Lerp(rigidbody2D.transform.position, target.position, .1f);
    }
}
