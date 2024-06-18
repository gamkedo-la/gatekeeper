using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorController : MonoBehaviour
{

    [SerializeField] private Transform closedPositionWaypoint;

    [SerializeField] private GameObject player;

    private bool closed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().getPickUpDeaddrop() == true && !closed)
        {
            transform.position = closedPositionWaypoint.position;
            closed = true;
        }
    }

}
