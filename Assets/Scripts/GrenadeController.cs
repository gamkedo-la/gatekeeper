using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject explosionPrefabFX;
    [SerializeField] private AudioSource explosionSfx;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject blast = GameObject.Instantiate<GameObject>(explosionPrefabFX);
        blast.transform.position = transform.position;
        Collider2D[] nearBy = Physics2D.OverlapCircleAll(transform.position, 3f);
        for (int i = 0; i < nearBy.Length; i++)
        {
            //Debug.Log(nearBy[i].name);
            PlayerController pcScript = nearBy[i].GetComponent<PlayerController>();
            if (pcScript != null)
            {
                pcScript.takeDamage(10);
            }
        }
        Destroy(gameObject);

    }

}
