using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject characterData;

    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    private Slider hpbar;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    // I-frame (frame de invencilidade)
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public bool isFieldActive;


    void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        hpbar = GameObject.FindGameObjectWithTag("hpbar").GetComponent<Slider>();
        isFieldActive = false;
    }

    void Start()
    {
        hpbar.maxValue = currentHealth;
        hpbar.value = currentHealth;
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }// se o tempo de imortalidade chegar a 0, redefino como não estando invencivel no momento.
        else if (isInvincible)
        {
            isInvincible = false;
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience = experienceCap;
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log("TOMOU DANO");
        Debug.Log(isFieldActive);
        // se o player não estiver invencivel e se o escudo não estiver ativo, começa o frame de invencibilidade
        if (!isFieldActive)
        {
            if (!isInvincible)
            {
                currentHealth -= dmg;

                hpbar.value = currentHealth;

                invincibilityTimer = invincibilityDuration;
                isInvincible = true;

                if (currentHealth <= 0)
                {
                    Kill();
                }
            }
        }
    }

    public void Kill()
    {
        Debug.Log("O JOGADOR MORREU");
        SceneManager.LoadScene("GameOver");
    }


}
