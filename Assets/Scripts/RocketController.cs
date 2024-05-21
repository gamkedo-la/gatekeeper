using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform rocketRangeStart;
    [SerializeField] private Transform rocketRangeEnd;
    [SerializeField] private Rigidbody2D rigidbody2D;
    private float flyCount;
    private float flyCountMax;

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    public Transform getTarget() { return this.target; }

    void Start()
    {
        flyCount = 0;
        flyCountMax = 8;
        target.position = new Vector3(Random.Range(rocketRangeStart.position.x, rocketRangeEnd.position.x), Random.Range(rocketRangeStart.position.y, rocketRangeEnd.position.y), 0);
    }

    void Awake()
    {
        rigidbody2D.velocity = new Vector2(0, 10);

    }

    void FixedUpdate()
    {
        flyCount += Time.deltaTime;
        if(flyCount > flyCountMax)
        {
            rigidbody2D.transform.position = Vector3.Lerp(rigidbody2D.transform.position, target.position, .10f);
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
