using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    Transform player;
    private Vector3 originalScale;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Movimentação do inimigo
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime);

        // Verificar a direção do movimento e flipar o sprite
        if (player.position.x < transform.position.x)
        {
            // Virar para a esquerda
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            // Virar para a direita
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
}
