using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Temp_ScanbarController : MonoBehaviour
{

    [SerializeField]
    private float speed = 40.0f;
    [SerializeField]
    private float targetY;
    [SerializeField]
    private float targetTopY;
    [SerializeField]
    private float targetBottomY;



    // Start is called before the first frame update
    void Start()
    {
        targetTopY= 64;
        targetBottomY= -242;
        targetY = targetBottomY;
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.localPosition.y >= targetTopY)
        {
            targetY = targetBottomY;
        }
       
        if (transform.localPosition.y <= targetBottomY)
        {
            targetY = targetTopY;
        }

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            Mathf.MoveTowards(transform.localPosition.y, targetY, speed * Time.deltaTime),
            transform.localPosition.z);
    }
}
