using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private Transform target { get; set; }
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform rocketRangeStart { get; set; }
    [SerializeField] private Transform rocketRangeEnd { get; set; }
    private bool hitTarget = false;



    void Start()
    {
    }

    void FixedUpdate()
    {
        rigidbody2D.transform.position = Vector3.Lerp(rigidbody2D.transform.position, target.position, .10f);

        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Vector3.Distance(transform.position, target.position) <= 1 && !hitTarget)
              {
                  target.position = new Vector3(Random.Range(rocketRangeStart.position.x, rocketRangeEnd.position.x), Random.Range(rocketRangeStart.position.y, rocketRangeEnd.position.y), 0);

                  hitTarget = true;
              }
    }
}
