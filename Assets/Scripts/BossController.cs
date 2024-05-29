using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject grenade;
    [SerializeField] private Transform rightArm;
    [SerializeField] private Transform leftArm;
    [SerializeField] private Transform grenadeSpawnPoint;
    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private Transform rocketSkyPoint;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private Rigidbody2D rigidbody2DBody;
    private Vector3 velocity;

    private bool facingRight = false;

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    void Start()
    {
        health = 1000;
        velocity = new Vector3(1.75f, 1.1f,0.0f);
    }

    private void FixedUpdate()
    {
        rigidbody2DBody.transform.position = Vector3.Lerp(rigidbody2DBody.transform.position, waypoints[3].position,.1f);
    }

    void Update()
    {
        Aim();

        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(grenade, grenadeSpawnPoint.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject rocketObject = Instantiate(rocket, rocketSpawnPoint.position, Quaternion.identity);
            rocketObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            rocketObject.GetComponentInChildren<RocketController>().setTarget(rocketSkyPoint.position);
        }


    }

    private void Aim()
    {
        float angle = Mathf.Atan2(player.transform.position.y - rightArm.position.y, player.transform.position.x - rightArm.position.x) * Mathf.Rad2Deg;

        if (!facingRight)
        {
            if (angle < 0) angle += 360;
            angle = Mathf.Clamp(angle, 90, 270);

            rightArm.rotation = Quaternion.Euler(180, -180, angle);
            leftArm.rotation = Quaternion.Euler(180, -180, angle);
        }
        else if (facingRight)
        {
            angle = Mathf.Clamp(angle, -90, 90);

            rightArm.rotation = Quaternion.Euler(0, 0, angle);
            leftArm.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
