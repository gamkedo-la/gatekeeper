using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool skipTutorialChecked = false;
    private float distanceZ;

    [SerializeField] private Transform player;

    void Start()
    {
        distanceZ = Camera.main.transform.position.z - player.transform.position.z;
    }

    void LateUpdate()
    {
        float lerpValue = Time.deltaTime * 5f;
        if (skipTutorialChecked == false)
        {
            skipTutorialChecked = true;
            if(PlayerController.finishedTutorial)
            {
                lerpValue = 1.0f;
            }
        }
        Vector3 targetPosition = new Vector3(player.transform.position.x, 3.0f, player.transform.position.z + distanceZ);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, lerpValue);
    }
}
