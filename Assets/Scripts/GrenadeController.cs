using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        rigidbody2D.velocity = new Vector2(Random.RandomRange(-8,-2), 10);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
