using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class PauseMenuManager : MonoBehaviour
{
    [Header("Configuração UI")]
    public GameObject pauseMenuPanel;

    private bool isGamePaused = false;

    void Start()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f;
        Debug.Log("PauseMenuManager iniciado. Tempo de jogo normal."); // Nova linha
    }

    void Update()
    {
        Debug.Log("Update de PauseMenuManager está rodando."); 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Tecla ESC pressionada!");
            if (isGamePaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true); 
        }
        Time.timeScale = 0f; 
        Debug.Log("Jogo Pausado. Time.timeScale = 0.");
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); 
        }
        Time.timeScale = 1f; 
        Debug.Log("Jogo Pausado. Time.timeScale = 0.");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        
        SceneManager.LoadScene("Menu");
    }
}