using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Função que muda a cena, recebe o nome da cena como parâmetro
    public void ChangeScene(string sceneName)
    {
        // Verifica se a cena está disponível e carrega a nova cena
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("A cena " + sceneName + " não está disponível.");
        }
    }
}
