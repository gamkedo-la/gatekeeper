using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private Slider slider;

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

    [Header("Attack Values")]
    [SerializeField] private int grenadeAttackAmount;
    [SerializeField] private int rocketAttackAmount;


    private Vector3 velocity;

    private bool facingRight = false;

    public void takeDamage(int damage)
    {
        health -= damage;
        slider.value -= damage;
        if (slider.value <= 0)
        {
            slider.gameObject.GetComponentInChildren<Image>().enabled = false;
        }
    }

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = health;
        slider.value = health;
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
            StartCoroutine("GrenadeAttack", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine("RocketAttack", 0.5f);
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

    IEnumerator RocketAttack(float duration)
    {
        for(int i = 0; i < rocketAttackAmount; i++)
        {
            yield return new WaitForSeconds(duration);
            GameObject rocketObject = Instantiate(rocket, rocketSpawnPoint.position, Quaternion.identity);
            rocketObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            rocketObject.GetComponentInChildren<RocketController>().setTarget(rocketSkyPoint.position);
        }
    }

    IEnumerator GrenadeAttack(float duration)
    {

        for (int i = 0; i < grenadeAttackAmount; i++)
        {
            yield return new WaitForSeconds(duration);
            Instantiate(grenade, grenadeSpawnPoint.position, Quaternion.identity);
        }
    }


}
