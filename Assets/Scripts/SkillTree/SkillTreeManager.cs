using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public GameObject nodePrefab;
    private SkillNode rootNode;
    public PlayerScriptableObject player;

    private static SkillTreeManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Não destrói este objeto ao mudar de cena
        }
    }

    void Start()
    {
        rootNode = CreateSkillTree();
        GenerateUI(rootNode, new Vector2(0, 206), 0);

        // Carregar o estado salvo
        LoadPlayerAttributes();
        LoadTreeProgress(rootNode);

        rootNode.UnlockNode(); // Libera a raiz inicialmente
    }

    // Método para criar a skill tree
    private SkillNode CreateSkillTree()
    {
        SkillNode root = new SkillNode("A", "ATK +10%");
        SkillNode nodeB = new SkillNode("B", "HP +20%");
        SkillNode nodeC = new SkillNode("C", "ATK +5%");

        SkillNode nodeD = new SkillNode("D", "ATK +5%");
        SkillNode nodeE = new SkillNode("E", "ATK +10%");
        SkillNode nodeF = new SkillNode("F", "SPD +5%");
        SkillNode nodeG = new SkillNode("G", "SPD +5%");
        SkillNode nodeH = new SkillNode("H", "HP +15%");
        SkillNode nodeI = new SkillNode("I", "ATK +10%");

        SkillNode nodeJ = new SkillNode("J", "ATK +20%");
        SkillNode nodeK = new SkillNode("K", "ATK +15%");
        SkillNode nodeL = new SkillNode("L", "SPD +10%");
        SkillNode nodeM = new SkillNode("M", "SPD +10%");
        SkillNode nodeN = new SkillNode("N", "HP +25%");
        SkillNode nodeO = new SkillNode("O", "ATK +15%");

        ConnectNodes(root, nodeB, nodeC);
        ConnectNodes(nodeB, nodeD, nodeE, nodeF);
        ConnectNodes(nodeC, nodeG, nodeH, nodeI);
        ConnectNodes(nodeD, nodeJ, nodeK);
        ConnectNodes(nodeE, nodeJ, nodeK, nodeL);
        ConnectNodes(nodeF, nodeK, nodeL);
        ConnectNodes(nodeG, nodeM, nodeN);
        ConnectNodes(nodeH, nodeM, nodeN, nodeO);
        ConnectNodes(nodeI, nodeN, nodeO);

        return root;
    }

    private void ConnectNodes(SkillNode parent, params SkillNode[] children)
    {
        foreach (var child in children)
        {
            parent.children.Add(child);
            child.parent = parent;
        }
    }

    private void GenerateUI(SkillNode node, Vector2 position, int depth)
    {
        if (node == null) return;

        GameObject buttonObject = Instantiate(nodePrefab, transform);
        buttonObject.GetComponent<RectTransform>().anchoredPosition = position;
        Button button = buttonObject.GetComponent<Button>();

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonText.text = node.description; // Alterado para usar a descrição da habilidade
        button.onClick.AddListener(() => OnNodeClicked(node));

        node.button = button; // Armazena referência ao botão no nó

        Dictionary<string, Vector2> nodePositions = new Dictionary<string, Vector2>()
        {
            { "A", new Vector2(0, 206) },
            { "B", new Vector2(-250, 116) },
            { "C", new Vector2(250, 116) },
            { "D", new Vector2(-350, 16) },
            { "E", new Vector2(-250, 16) },
            { "F", new Vector2(-150, 16) },
            { "G", new Vector2(150, 16) },
            { "H", new Vector2(250, 16) },
            { "I", new Vector2(350, 16) },
            { "J", new Vector2(-450, -84) },
            { "K", new Vector2(-250, -84) },
            { "L", new Vector2(-50, -84) },
            { "M", new Vector2(50, -84) },
            { "N", new Vector2(250, -84) },
            { "O", new Vector2(450, -84) }
        };

        if (nodePositions.ContainsKey(node.id))
        {
            buttonObject.GetComponent<RectTransform>().anchoredPosition = nodePositions[node.id];
        }

        foreach (var child in node.children)
        {
            GenerateUI(child, nodePositions[child.id], depth + 1);
        }
    }

    private void OnNodeClicked(SkillNode node)
    {
        if (!node.IsUnlocked()) // Verifica se o nó não está desbloqueado
        {
            Debug.Log($"Tentativa de clicar em um nó bloqueado: {node.id}");
            return;
        }

        // Aplica o efeito da habilidade no jogador
        node.ApplyEffect(player);

        // Salvar o estado do nó (habilidade desbloqueada)
        PlayerPrefs.SetInt(node.id + "_unlocked", 1);

        // Salvar os atributos do jogador (ATK, HP, etc)
        SavePlayerAttributes();

        Debug.Log($"Nó {node.id} clicado. Efeito aplicado: {node.description}");

        // Limpa a árvore, bloqueando todos os nós
        BlockAllNodes(rootNode, node); // Altera a forma de bloqueio

        // Desbloquear o nó clicado e seus descendentes
        node.UnlockDescendants();
    }

    // Método para bloquear todos os nós usando busca em largura
    private void BlockAllNodes(SkillNode root, SkillNode currentNode)
    {
        Queue<SkillNode> queue = new Queue<SkillNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            SkillNode node = queue.Dequeue();
            if (node != currentNode) // Não bloqueia o nó atual
            {
                node.LockNode();
            }

            // Adiciona os filhos à fila
            foreach (var child in node.children)
            {
                queue.Enqueue(child);
            }
        }
    }

    // Função para salvar os atributos do jogador no PlayerPrefs
    private void SavePlayerAttributes()
    {
        PlayerPrefs.SetFloat("MaxHealth", player.MaxHealth);
        PlayerPrefs.SetFloat("MoveSpeed", player.MoveSpeed);
        PlayerPrefs.SetFloat("Might", player.Might);
        PlayerPrefs.SetFloat("ProjectileSpeed", player.ProjectileSpeed);
        PlayerPrefs.Save(); // Salva imediatamente
    }

    // Função para carregar os atributos do jogador do PlayerPrefs
    private void LoadPlayerAttributes()
    {
        if (PlayerPrefs.HasKey("MaxHealth"))
            player.MaxHealth = PlayerPrefs.GetFloat("MaxHealth");

        if (PlayerPrefs.HasKey("MoveSpeed"))
            player.MoveSpeed = PlayerPrefs.GetFloat("MoveSpeed");

        if (PlayerPrefs.HasKey("Might"))
            player.Might = PlayerPrefs.GetFloat("Might");

        if (PlayerPrefs.HasKey("ProjectileSpeed"))
            player.ProjectileSpeed = PlayerPrefs.GetFloat("ProjectileSpeed");
    }

    // Função para carregar o progresso da árvore de habilidades
    private void LoadTreeProgress(SkillNode node)
    {
        if (PlayerPrefs.HasKey(node.id + "_unlocked") && PlayerPrefs.GetInt(node.id + "_unlocked") == 1)
        {
            node.UnlockNode(); // Desbloqueia o nó
        }

        foreach (var child in node.children)
        {
            LoadTreeProgress(child);
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); // Apaga todas as chaves salvas
    }
}
