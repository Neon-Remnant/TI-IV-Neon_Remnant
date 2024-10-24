using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : WeaponController
{
    private Transform player;  

    protected override void Start()
    {
        base.Start();
        // Encontra o jogador
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBullet = Instantiate(weaponData.Prefab);
        spawnedBullet.transform.position = transform.position;

        // Calcula direção do ataque
        Vector3 attackDirection = (player.position - transform.position).normalized;
        spawnedBullet.GetComponent<BulletBehaviour>().DirectionChecker(attackDirection);
    }
}