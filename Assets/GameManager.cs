using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NpcController npcPrefab;
    [SerializeField]
    private List<NpcController> npcControllers = new List<NpcController>();

    [SerializeField]
    private GameObject scannerPanel;

    [SerializeField]
    private WaypointController spawnWaypoint;
    [SerializeField]
    private WaypointController gateWaypoint;
    [SerializeField]
    private WaypointController cityWaypoint;

    private float timeCount = 0f;
    [SerializeField]
    private int npcCount = 5;


    private List<string> names = new List<string>();
    

    public void Start()
    {
        names.Add("John");
        names.Add("Mary");
        names.Add("George");
        names.Add("Peter");
        names.Add("Michael");
    }

    public void Update()
    {
        
        timeCount += Time.deltaTime;
        if (timeCount >= 2f && npcControllers.Count < npcCount)
        {
            npcControllers.Add(CreatePeasant(gateWaypoint.transform));
            timeCount = 0;
        }

        if (Input.GetButtonDown("Jump") && gateWaypoint.GetComponent<WaypointController>().getNpcsAtWaypoint().Count >= 1)
        {
            gateWaypoint.getNpcsAtWaypoint()[0].MoveToTransform(cityWaypoint.transform);
        }
        
    }

    public NpcController CreatePeasant(Transform targetLocation)
    {
        NpcController npc = Instantiate(npcPrefab, spawnWaypoint.transform.position , Quaternion.identity);
        npc.MoveToTransform(targetLocation);
        npc.setScannerPanel(scannerPanel);
        npc.npcName = names[Random.Range(0, names.Count)];
        return npc;


    }
}
