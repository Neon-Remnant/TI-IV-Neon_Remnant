using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : ProjectileWeaponBehaviour
{
    private Vector3 moveDirection;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        // Move a bala para a direção desejada
        transform.position += moveDirection * weaponData.Speed * Time.deltaTime;
    }

    public void DirectionChecker(Vector3 direction)
    {
        moveDirection = direction;  // Direção da bala
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(weaponData.Damage);
            Destroy(gameObject); // Destruir a bala ao colidir com o jogador
        }

        // Ignorar colisão com inimigos
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        // Destruir a bala ao colidir com o Tilemap Collider 2D
        if (collision.gameObject.CompareTag("Tilemap"))
        {
            Destroy(gameObject); // Destruir a bala ao colidir com o Tilemap
        }
    }
}
