using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject endPanel;
    // Start is called before the first frame update
    // Defina o nome do botão que irá reiniciar o jogo na interface Unity

    void Update()
    {
        // Verifica se o botão especificado foi pressionado
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    
}
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ResetGame()
    {
       
    SceneManager.LoadSceneAsync(0);

    }
    
    
    public void LoseGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Over ...";
    }
    public void WinGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Win ...";
        
    }
}

