using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantFactory : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform gatepoint;
    public PeasantController peasantPrefab;
    public float timeCount = 0f;
    public int peasantCount = 5;
    public List<PeasantController> peasantControllers = new List<PeasantController>();


    public void Start()
    {
        peasantControllers.Add(CreatePeasant(gatepoint));

    }

    public void Update()
    {
        timeCount += Time.deltaTime;
        if(timeCount >= 2f && peasantControllers.Count < peasantCount)
        {
            peasantControllers.Add(CreatePeasant(peasantControllers[peasantControllers.Count - 1].transform));
            timeCount = 0;
        }
    }


    public PeasantController CreatePeasant(Transform targetLocation)
    {
        PeasantController peasant = Instantiate(peasantPrefab, spawnPoint.position, Quaternion.identity);
        peasant.movementTarget = targetLocation;
        return peasant;


    }
}