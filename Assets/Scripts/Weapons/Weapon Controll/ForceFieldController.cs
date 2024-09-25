using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldController : WeaponController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedField = Instantiate(weaponData.Prefab);
        spawnedField.transform.position = transform.position;
        spawnedField.transform.parent = transform;
    }

}
