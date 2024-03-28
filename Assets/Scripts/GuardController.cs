using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : NpcController
{
    [SerializeField] 
    private BulletController bulletPrefab;

    [SerializeField]
    private Transform muzzleTransform;

    [SerializeField]
    private Transform target;

    void Start()
    {
        BulletController bullet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.identity);
        bullet.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
