using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : NpcController
{

    [SerializeField]
    private GameObject civilianDetector;

    [SerializeField] 
    private float detectCivilianDistance;

    [SerializeField]
    private LayerMask civilianLayerMask;

    [SerializeField]
    private Transform moveTarget;

    [SerializeField]
    private bool canAttack;

    [SerializeField]
    private float attackTimeoutDuration;

    [SerializeField]
    private float attackTimer;

    void Start()
    {
        canAttack = true;
        attackTimer = 0;
    }

    private void FixedUpdate()
    {
        attackTimer += Time.deltaTime;

        RaycastHit2D detectCivilian = Physics2D.Raycast(civilianDetector.transform.position, Vector2.right, detectCivilianDistance, civilianLayerMask);

        if (attackTimer >= attackTimeoutDuration && !canAttack)
        {
            attackTimer = 0;
            isMoving = true;
            canAttack = true;
            
        }

        if (detectCivilian.collider != null)
        {

            Debug.DrawRay(civilianDetector.transform.position, Vector2.right * detectCivilianDistance, Color.green);
            if(canAttack)
            {
                isMoving = false;
                canAttack = false;
                GetComponent<Rigidbody2D>().AddForce(new Vector3(5.0f, 0, 0), ForceMode2D.Impulse);


            }

        }
        else
        {
            Debug.DrawRay(civilianDetector.transform.position, Vector2.right * detectCivilianDistance, Color.red);
        }

    }

    void Update()
    {
        if(isMoving)
        {
            MoveToTransform(moveTarget);
        }

    }


}
