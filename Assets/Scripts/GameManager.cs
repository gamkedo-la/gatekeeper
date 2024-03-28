using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CivilianController civilianPrefab;

    [SerializeField]
    private EnemyController enemyPrefab;

    [SerializeField]
    private List<CivilianController> civilianControllers = new List<CivilianController>();

    [SerializeField]
    private List<EnemyController> enemyControllers = new List<EnemyController>();

    [SerializeField]
    private List<GuardController> guardControllers = new List<GuardController>();

    [SerializeField]
    private GameObject scannerPanel;

    [SerializeField]
    private GameObject scannerDiseaseImage;

    [SerializeField]
    private WaypointController spawnWaypoint;
    [SerializeField]
    private WaypointController gateWaypoint;
    [SerializeField]
    private WaypointController cityWaypoint;

    private float timeCount = 0f;
    [SerializeField]
    private int civilianCount = 5;


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
        if (timeCount >= 2f && civilianControllers.Count < civilianCount)
        {
            civilianControllers.Add(CreateCivilian(gateWaypoint.transform));
            timeCount = 0;
        }

        if (Input.GetButtonDown("Jump") && gateWaypoint.GetComponent<WaypointController>().getNpcsAtWaypoint().Count >= 1)
        {
            gateWaypoint.getNpcsAtWaypoint()[0].MoveToTransform(cityWaypoint.transform);
        }
        
    }

    public CivilianController CreateCivilian(Transform targetLocation)
    {
        CivilianController civilian = Instantiate(civilianPrefab, spawnWaypoint.transform.position , Quaternion.identity);
        civilian.MoveToTransform(targetLocation);
        civilian.setScannerPanel(scannerPanel);
        civilian.npcName = names[Random.Range(0, names.Count)];
        civilian.setScannerDiseaseImage(scannerDiseaseImage);
        return civilian;


    }
}
