using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    public float destroyAfterSeconds;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    protected PlayerStats playerStats;
    private EfeitosSonoros efeitosSonoros;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;

        playerStats = FindObjectOfType<PlayerStats>();
        efeitosSonoros = FindObjectOfType<EfeitosSonoros>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Ativa o escudo ao criar
        if (playerStats != null)
        {
            playerStats.isFieldActive = true;
        }

        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnDestroy()
    {
        // Desativa o escudo quando o objeto é destruído
        if (playerStats != null)
        {
            playerStats.isFieldActive = false;
        }

        if (efeitosSonoros != null)
        {
            efeitosSonoros.TocarSomDoEscudo();  // Reutilizamos o som do escudo para desativação
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}
