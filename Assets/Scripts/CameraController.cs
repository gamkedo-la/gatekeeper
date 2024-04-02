using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float distanceZ;

    [SerializeField] private Transform player;

    void Start()
    {
        distanceZ = Camera.main.transform.position.z - player.transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + distanceZ);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, Time.deltaTime * 5f);
    }
}
