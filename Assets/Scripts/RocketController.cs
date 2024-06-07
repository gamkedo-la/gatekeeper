using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketController : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    [SerializeField] private Transform rocketRangeStart;
    [SerializeField] private Transform rocketRangeEnd;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject explosionPrefabFX;
    [SerializeField] private AudioSource explosionSfx;
    private float flyCount;
    private float flyCountMax;

    public void setTarget(Vector3 target)
    {
        this.target = target;
    }

    public Vector3 getTarget() { return this.target; }

    void Start()
    {
        flyCount = 0;
        flyCountMax = 2;
        target = new Vector3(Random.Range(rocketRangeStart.position.x, rocketRangeEnd.position.x), Random.Range(rocketRangeStart.position.y, rocketRangeEnd.position.y), 0);
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
            rigidbody2D.transform.position = Vector3.MoveTowards(rigidbody2D.transform.position, target, 1f);
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject blast = GameObject.Instantiate<GameObject>(explosionPrefabFX);
        blast.transform.position = transform.position;
        Collider2D[] nearBy = Physics2D.OverlapCircleAll(transform.position, 3f);
        for (int i = 0; i < nearBy.Length;i++)
        {
            //Debug.Log(nearBy[i].name);
            PlayerController pcScript = nearBy[i].GetComponent<PlayerController>();
            if(pcScript != null)
            {
                pcScript.takeDamage(10);
            }
        }
        Destroy(gameObject);

    }


}
