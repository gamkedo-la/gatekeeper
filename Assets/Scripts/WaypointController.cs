using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WaypointController : MonoBehaviour
{

    [SerializeField]
    private List<NpcController> npcsAtWaypoint = new List<NpcController>();

    public List<NpcController> getNpcsAtWaypoint()
    {
        return npcsAtWaypoint;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EnteredTrigger");
        npcsAtWaypoint.Add(collision.gameObject.GetComponent<NpcController>());
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ExitedTrigger");
        npcsAtWaypoint.Remove(collision.gameObject.GetComponent<NpcController>());
    }

}
