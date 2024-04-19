using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform rightArm;
    [SerializeField] private Transform leftArm;

    private bool facingRight = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        float angle = Mathf.Atan2(player.transform.position.y - rightArm.position.y, player.transform.position.x - rightArm.position.x) * Mathf.Rad2Deg;

        if (!facingRight)
        {
            if (angle < 0) angle += 360;
            angle = Mathf.Clamp(angle, 90, 270);

            rightArm.rotation = Quaternion.Euler(180, 0, -angle);
            leftArm.rotation = Quaternion.Euler(180, 0, -angle);
        }
        else if (facingRight)
        {
            angle = Mathf.Clamp(angle, -90, 90);

            rightArm.rotation = Quaternion.Euler(0, 0, angle);
            leftArm.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
