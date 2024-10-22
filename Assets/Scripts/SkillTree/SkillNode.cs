using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode
{
    public string name;
    public string id;
    public List<SkillNode> children = new List<SkillNode>();
    public Button button; // Referência ao botão UI
    public SkillNode parent; // Referência ao nó pai
    private bool isUnlocked;

    public string description; // Descrição do efeito da habilidade

    public SkillNode(string name, string description)
    {
        this.id = name;
        this.name = name;
        this.description = description;
        isUnlocked = false; // Inicialmente bloqueado
    }

    public void UnlockNode()
    {
        isUnlocked = true;
        button.interactable = true;
    }

    public void LockNode()
    {
        isUnlocked = false;
        button.interactable = false;
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }

    // Aplica o efeito da habilidade ao jogador
    public void ApplyEffect(PlayerScriptableObject player)
    {
        if (description.Contains("ATK"))
        {
            player.Might += ParseEffect(description);
        }
        else if (description.Contains("HP"))
        {
            player.MaxHealth += ParseEffect(description);
        }
        else if (description.Contains("SPD"))
        {
            player.MoveSpeed += ParseEffect(description);
        }
    }

    private float ParseEffect(string effectDescription)
    {
        // Supondo que a descrição tenha o formato "ATK +10%" ou "HP +20%"
        string[] parts = effectDescription.Split(' ');
        if (parts.Length > 1 && float.TryParse(parts[1].Replace("%", ""), out float value)) // Corrigido para usar parts[1]
        {
            return value; // Retorna o valor como float
        }
        return 0f; // Retorna 0 caso não consiga parsear
    }

    public void UnlockDescendants()
    {
        foreach (var child in children)
        {
            child.UnlockNode();
            child.UnlockDescendants();
        }
    }

    // Bloqueia todos os descendentes, exceto o nó especificado
    public void LockDescendants(SkillNode exceptNode = null)
    {
        foreach (var child in children)
        {
            if (child != exceptNode)
            {
                child.LockNode();
                child.LockDescendants();
            }
        }
    }
}
