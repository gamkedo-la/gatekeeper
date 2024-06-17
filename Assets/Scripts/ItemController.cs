using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            player.GetComponent<PlayerController>().setPickUpDeaddrop(true);
        }
    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().getPickUpDeaddrop() == true)
        {
            Destroy(gameObject);
        } 
    }
}
