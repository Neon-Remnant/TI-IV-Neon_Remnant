using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : WeaponController
{

    private EfeitosSonoros efeitosSonoros;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        efeitosSonoros = FindObjectOfType<EfeitosSonoros>();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedDisk = Instantiate(weaponData.Prefab);
        spawnedDisk.transform.position = transform.position;
        spawnedDisk.GetComponent<DiskBehaviour>().DirectionChecker(pm.lastMoveVector);

        if (efeitosSonoros != null)
        {
            efeitosSonoros.TocarSomDoTiro();  // Toca o som do escudo
        }
    }
}
