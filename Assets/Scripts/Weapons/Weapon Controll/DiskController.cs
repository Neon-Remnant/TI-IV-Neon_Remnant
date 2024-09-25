using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedDisk = Instantiate(weaponData.Prefab);
        spawnedDisk.transform.position = transform.position;
        spawnedDisk.GetComponent<DiskBehaviour>().DirectionChecker(pm.lastMoveVector);
    }
}
