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
    [SerializeField] private GameObject player;



    private Vector3 lastPositionPerSec;

    private float flyCount;
    private float flyCountMax;

    private bool finalTarget = false;

    public void setRocketRangeStart(Transform rocketRangeStart)
    {
        this.rocketRangeStart = rocketRangeStart;
    }
    public void setRocketRangeEnd(Transform rocketRangeEnd)
    {
        this.rocketRangeEnd = rocketRangeEnd;
    }

    public void setTarget(Vector3 target)
    {
        this.target = target;
    }

    public Vector3 getTarget() { return this.target; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flyCount = 0;
        flyCountMax = 2;
        lastPositionPerSec = transform.position;
        StartCoroutine(StalledCheck());

    }

    void Awake()
    {
        rigidbody2D.velocity = new Vector2(0, 10);

    }

    IEnumerator StalledCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            float dist = Vector3.Distance(lastPositionPerSec, transform.position);
            if (dist < 1.0f)
            {
                Explode();
            }
            lastPositionPerSec = transform.position;
        }
    }

    void FixedUpdate()
    {
        flyCount += Time.deltaTime;
        if(flyCount > flyCountMax)
        {
            if (!finalTarget)
            {
                target = new Vector3(Random.Range(rocketRangeStart.position.x, rocketRangeEnd.position.x), Random.Range(rocketRangeStart.position.y, rocketRangeEnd.position.y), 0);
                finalTarget = true;
            }
          
            rigidbody2D.transform.position = Vector3.MoveTowards(rigidbody2D.transform.position, target, 1f);
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    void Explode()
    {
        GameObject blast = GameObject.Instantiate<GameObject>(explosionPrefabFX);
        blast.transform.position = transform.position;

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().takeDamage(10);
        }
        Explode();

    }


}
