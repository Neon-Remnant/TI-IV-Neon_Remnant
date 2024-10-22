using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public GameObject nodePrefab; // Prefab do botão
    private SkillNode rootNode;

    void Start()
    {
        rootNode = CreateSkillTree();
        GenerateUI(rootNode, new Vector2(0, 206), 0); // Inicia a geração da UI com a raiz em (0,206)
        rootNode.UnlockNode(); // Libera a raiz inicialmente
    }

    // Método para criar a skill tree
    private SkillNode CreateSkillTree()
    {
        // Nível 1
        SkillNode root = new SkillNode("Habilidade A");

        // Nível 2
        SkillNode nodeB = new SkillNode("Habilidade B");
        SkillNode nodeC = new SkillNode("Habilidade C");

        // Nível 3
        SkillNode nodeD = new SkillNode("Habilidade D");
        SkillNode nodeE = new SkillNode("Habilidade E");
        SkillNode nodeF = new SkillNode("Habilidade F");
        SkillNode nodeG = new SkillNode("Habilidade G");
        SkillNode nodeH = new SkillNode("Habilidade H");
        SkillNode nodeI = new SkillNode("Habilidade I");

        // Nível 4
        SkillNode nodeJ = new SkillNode("Habilidade J");
        SkillNode nodeK = new SkillNode("Habilidade K");
        SkillNode nodeL = new SkillNode("Habilidade L");
        SkillNode nodeM = new SkillNode("Habilidade M");
        SkillNode nodeN = new SkillNode("Habilidade N");
        SkillNode nodeO = new SkillNode("Habilidade O");

        // Conectar o nível 2 ao nível 1
        ConnectNodes(root, nodeB, nodeC);

        // Conectar o nível 3 ao nível 2
        ConnectNodes(nodeB, nodeD, nodeE, nodeF);
        ConnectNodes(nodeC, nodeG, nodeH, nodeI);

        // Conectar o nível 4 ao nível 3
        ConnectNodes(nodeD, nodeJ, nodeK);
        ConnectNodes(nodeE, nodeJ, nodeK, nodeL);
        ConnectNodes(nodeF, nodeK, nodeL);
        ConnectNodes(nodeG, nodeM, nodeN);
        ConnectNodes(nodeH, nodeM, nodeN, nodeO);
        ConnectNodes(nodeI, nodeN, nodeO);

        return root;
    }

    // Método auxiliar para conectar os nós
    private void ConnectNodes(SkillNode parent, params SkillNode[] children)
    {
        foreach (var child in children)
        {
            parent.children.Add(child);
            child.parent = parent;
        }
    }

    // Método para gerar a UI
    private void GenerateUI(SkillNode node, Vector2 position, int depth)
    {
        // Verifica se o nó atual é nulo
        if (node == null)
        {
            Debug.LogError("Node is null!");
            return;
        }

        // Cria o botão para o nó atual
        GameObject buttonObject = Instantiate(nodePrefab, transform);
        if (buttonObject == null)
        {
            Debug.LogError("Button prefab could not be instantiated!");
            return;
        }

        // Define a posição do botão
        buttonObject.GetComponent<RectTransform>().anchoredPosition = position;

        // Obtém o componente Button
        Button button = buttonObject.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component is missing!");
            return;
        }

        // Obtém o componente TMP_Text
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText == null)
        {
            Debug.LogError("TMP_Text component is missing in button!");
            return;
        }

        // Define o texto do botão como o nome do nó
        buttonText.text = node.name;
        button.onClick.AddListener(() => OnNodeClicked(node)); // Adiciona listener ao botão

        node.button = button; // Armazena referência ao botão no nó

        // Definindo posições fixas para cada nó
        Dictionary<string, Vector2> nodePositions = new Dictionary<string, Vector2>(){
            { "Habilidade A", new Vector2(0, 206) },
            { "Habilidade B", new Vector2(-250, 116) },
            { "Habilidade C", new Vector2(250, 116) },
            { "Habilidade D", new Vector2(-350, 16) },
            { "Habilidade E", new Vector2(-250, 16) },
            { "Habilidade F", new Vector2(-150, 16) },
            { "Habilidade G", new Vector2(150, 16) },
            { "Habilidade H", new Vector2(250, 16) },
            { "Habilidade I", new Vector2(350, 16) },
            { "Habilidade J", new Vector2(-450, -84) },
            { "Habilidade K", new Vector2(-250, -84) },
            { "Habilidade L", new Vector2(-50, -84) },
            { "Habilidade M", new Vector2(50, -84) },
            { "Habilidade N", new Vector2(250, -84) },
            { "Habilidade O", new Vector2(450, -84) }
        }; // funcionou amem

        // Definindo a posição para o nó atual com base no seu nome
        if (nodePositions.ContainsKey(node.name))
        {
            buttonObject.GetComponent<RectTransform>().anchoredPosition = nodePositions[node.name];
        }
        else
        {
            Debug.LogError($"No fixed position defined for node: {node.name}");
            return;
        }

        // Gera UI para os filhos
        foreach (var child in node.children)
        {
            // Chama recursivamente para gerar a UI dos filhos
            GenerateUI(child, nodePositions[child.name], depth + 1);
        }
    }

    // Método chamado quando um nó é clicado
    private void OnNodeClicked(SkillNode node)
    {
        if (!node.IsUnlocked()) // Verifica se o nó não está bloqueado
        {
            Debug.Log($"Attempted to click a locked node: {node.name}");
            return;
        }

        // Log do nó clicado
        Debug.Log($"Clicked on node: {node.name}");

        // Se o nó clicado for "Habilidade A", não executa a busca
        if (node.name == "Habilidade A")
        {
            node.UnlockDescendants(); // Desbloqueia descendentes da raiz
            return; // Retorna para não bloquear outros nós
        }

        // Limpa a árvore, bloqueando todos os nós
        BlockAllNodes(rootNode);

        // Desbloquear o nó clicado e seus descendentes
        node.UnlockDescendants();

        // Bloquear o nó clicado para impedir nova interação
        node.LockNode();
        Debug.Log($"Locked clicked node: {node.name}");
    }

    // Método para bloquear todos os nós
    private void BlockAllNodes(SkillNode node)
    {
        if (node == null) return;

        node.LockNode(); // Bloqueia o nó atual

        // Chama recursivamente para bloquear os filhos
        foreach (var child in node.children)
        {
            BlockAllNodes(child);
        }
    }
}
