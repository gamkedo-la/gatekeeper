using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BossController : MonoBehaviour
{

    public enum BossState
    {
        Idle,
        Grenades,
        Rockets,
        Jump,
        GunArmAim,
        NumStates
    }

    [SerializeField] private BossState state;
    private float timeUntilStateChange = 0f;
    private int goToWaypoint = 0;
    private int destinationWaypoint = 0;

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
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private int grenadeAttackAmount;
    [SerializeField] private int rocketAttackAmount;
    [SerializeField] private int gunArmAttackAmount;

    [SerializeField] private float secAfterAttack;

    private bool firstState = true;

    private bool bossFightStarted = false;


    private Vector3 velocity;

    private bool facingRight = false;

    public void setBossFightStarted(bool bossFightStarted)
    {
        this.bossFightStarted = bossFightStarted;
    }

    public bool getBossFightStarted()
    {
        return bossFightStarted;
    }

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
        if (state == BossState.Jump)
        {
            rigidbody2DBody.transform.position = Vector3.MoveTowards(rigidbody2DBody.transform.position, waypoints[goToWaypoint].position, 1f);
            float distToWaypoint = Vector3.Distance(rigidbody2DBody.transform.position, waypoints[goToWaypoint].position);
            if (distToWaypoint < 0.2f)
            {
                rigidbody2DBody.transform.position = waypoints[goToWaypoint].position;
                if (goToWaypoint == waypoints.Count - 1) // sky is last point in list
                {
                    goToWaypoint = destinationWaypoint;
                }
                else
                {

                    timeUntilStateChange = 0f;
                }
            }
        }
    }
    void Update()
    {

        float distToPlayer = Vector3.Distance(rigidbody2DBody.position,player.transform.position);
        //Debug.Log("Dist to Player: " + distToPlayer);

        if (distToPlayer < 5.0f)
        {
            timeUntilStateChange = Random.Range(2.5f, 3.0f);
            state = BossState.Jump;
            Jump();
        }

        if(health <= 0)
        {
            SceneManager.LoadScene(3);
        }

        flipFacingDirection();

        if (bossFightStarted)
        {
            BossStateMachine();
        }
        

    }

    private void Jump()
    {
        int prevWaypoint = destinationWaypoint;
        ResetAim();
        while (prevWaypoint == destinationWaypoint)
        {
            destinationWaypoint = Random.Range(0, waypoints.Count - 1); // minus one to exclude sky point
        }
        goToWaypoint = waypoints.Count - 1;
    }

    private void BossStateMachine()
    {
        
        timeUntilStateChange -= Time.deltaTime;
        //Code inside here only happens when we start a new state
        if (timeUntilStateChange < 0)
        {
            BossState previousState = state;
            //Default but certain attacks will override
            timeUntilStateChange = Random.Range(2.5f, 3.0f);
            while (previousState == state)
            {
                state = (BossState)Random.Range(0, (int)BossState.NumStates);
            }
            
            float grenadeDelay = 0.5f;
            float rocketDelay = 0.5f;

            
            if (firstState)
            {
                state = BossState.Jump;
                firstState = false;
            }
            

            switch (state)
            {
                case BossState.Idle:
                    ResetAim();
                    break;
                case BossState.Grenades:
                    ResetAim();
                    StartCoroutine("GrenadeAttack", grenadeDelay);
                    timeUntilStateChange = grenadeAttackAmount * grenadeDelay + secAfterAttack;
                    break;
                case BossState.Rockets:
                    ResetAim();
                    StartCoroutine("RocketAttack", rocketDelay);
                    timeUntilStateChange = rocketAttackAmount * rocketDelay + secAfterAttack;
                    break;
                case BossState.Jump:
                    Jump();
                    break;
                case BossState.GunArmAim:
                    Aim();
                    StartCoroutine("GunArmAttack", 0.2f);
                    break;
                default:
                    Debug.Log("Warning invalid state: " + state);
                    break;
            }

            Debug.Log("Boss state is now: " + state);
        }
        //Code below here happens every frame

        if (state == BossState.GunArmAim)
        {
            Aim();
        }
    }

    private void flipFacingDirection()
    {
        if (player.transform.position.x < rigidbody2DBody.transform.position.x)
        {
            rigidbody2DBody.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            facingRight = false;
        }
        else if (player.transform.position.x > rigidbody2DBody.transform.position.x)
        {
            rigidbody2DBody.transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
            facingRight = true;
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

    private void ResetAim()
    {
        float angle = Mathf.Atan2(player.transform.position.y - rightArm.position.y, player.transform.position.x - rightArm.position.x) * Mathf.Rad2Deg;

        if (!facingRight)
        {
            if (angle < 0) angle += 360;
            angle = Mathf.Clamp(angle, 90, 270);

            rightArm.rotation = Quaternion.Euler(180, -180, 180);
            leftArm.rotation = Quaternion.Euler(180, -180, 180);
        }
        else if (facingRight)
        {
            angle = Mathf.Clamp(angle, -90, 90);

            rightArm.rotation = Quaternion.Euler(0, 0, 0);
            leftArm.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    IEnumerator GunArmAttack(float duration)
    {
        for (int i = 0; i < gunArmAttackAmount; i++)
        {
            yield return new WaitForSeconds(duration);
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        }
    }

    IEnumerator RocketAttack(float duration)
    {
        for (int i = 0; i < rocketAttackAmount; i++)
        {
            yield return new WaitForSeconds(duration);
            GameObject rocketObject = Instantiate(rocket, rocketSpawnPoint.position, Quaternion.identity);
            rocketObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            rocketObject.GetComponentInChildren<RocketController>().setTarget(rocketSkyPoint.position);
            rocketObject.GetComponentInChildren<RocketController>().setRocketRangeStart(player.transform.Find("RocketAttackBoundry/LeftBoundry"));
            rocketObject.GetComponentInChildren<RocketController>().setRocketRangeEnd(player.transform.Find("RocketAttackBoundry/RightBoundry"));
        }
    }

    IEnumerator GrenadeAttack(float duration)
    {
        for (int i = 0; i < grenadeAttackAmount; i++)
        {
            yield return new WaitForSeconds(duration);
            GameObject grenadeObject = Instantiate(grenade, grenadeSpawnPoint.position, Quaternion.identity);
            grenadeObject.GetComponent<GrenadeController>().setPlayer(player);
        }
    }


}
