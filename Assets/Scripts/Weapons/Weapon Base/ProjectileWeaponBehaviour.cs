using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all projectile behaviours [To be placed on a prefab of a weapon that is a projectile]
/// </summary>
public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // esquerda
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0) //baixo
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0) //cima
        {
            rotation.z = +45f;
        }
        else if (dirx > 0 && diry > 0) //direita cima
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) //direita baixo
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) //equerda cima
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry < 0) //equerda baixo
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //Reference the script from the collided collider and deal damage using TakeDamage()
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);     //Make sure to use currentDamage instead of weaponData.Damage in case any damage multipliers in the future
            ReducePierce();
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0){
            Destroy(gameObject);
        }
    }
}