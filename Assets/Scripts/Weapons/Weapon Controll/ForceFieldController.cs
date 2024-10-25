using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldController : WeaponController
{

    private EfeitosSonoros efeitosSonoros;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        efeitosSonoros = FindObjectOfType<EfeitosSonoros>();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedField = Instantiate(weaponData.Prefab);
        spawnedField.transform.position = transform.position;
        spawnedField.transform.parent = transform;

        if (efeitosSonoros != null)
        {
            efeitosSonoros.TocarSomDoEscudo();  // Toca o som do escudo
        }
    }

}
