using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode
{
    public string name;
    public List<SkillNode> children = new List<SkillNode>();
    public Button button; // Referência ao botão UI
    public SkillNode parent; // Referência ao nó pai
    private bool isUnlocked;

    public SkillNode(string name)
    {
        this.name = name;
        isUnlocked = false; // Inicialmente bloqueado
    }

    public void UnlockNode()
    {
        isUnlocked = true;
        button.interactable = true; // Torna o botão interativo
    }

    public void LockNode()
    {
        isUnlocked = false;
        button.interactable = false; // Bloqueia o botão
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }

    // Desbloqueia todos os descendentes do nó atual
    public void UnlockDescendants()
    {
        foreach (var child in children)
        {
            child.UnlockNode(); // Desbloqueia o nó filho
            child.UnlockDescendants(); // Chama recursivamente para os descendentes
        }
    }

    // Bloqueia todos os descendentes, exceto o nó fornecido
    public void LockDescendants(SkillNode exceptNode = null)
    {
        foreach (var child in children)
        {
            if (child != exceptNode)
            {
                child.LockNode(); // Bloqueia o nó filho
                child.LockDescendants(); // Chama recursivamente para os descendentes
            }
        }
    }
}
