using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject explosionPrefabFX;
    [SerializeField] private AudioSource explosionSfx;
    [SerializeField] private GameObject player;

    public void setPlayer(GameObject player)
    {
        this.player = player;
    }

    void Start()
    {


        if (player.transform.position.x < rigidbody2D.transform.position.x)
        {
            rigidbody2D.velocity = new Vector2(Random.Range(-8, 0), 10);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(Random.Range(0, 8), 10);
        }

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
