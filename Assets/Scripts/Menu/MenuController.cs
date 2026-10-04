using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject inGameMenuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject creditsPanel;

    private bool isPaused = false;
    private void Start()
    {
        
        Time.timeScale = 1f;
    }

    private void Update()
    {
        bool escapePressed = Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
        bool pPressed = Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame;

        if (escapePressed || pPressed)
        {
            if (inGameMenuPanel != null &&
               (gameOverPanel == null || !gameOverPanel.activeSelf) &&
               (mainMenuPanel == null || !mainMenuPanel.activeSelf))
            {
                if (isPaused)
                    CloseInGameMenu();
                else
                    OpenInGameMenu();
            }
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;



        SceneManager.LoadScene("save");

        if (mainMenuPanel != null) mainMenuPanel.SetActive(false); }

       

    public void OpenCredits()
    {
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do Jogo...");
        Application.Quit();
    }

    
    public void TriggerGameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (inGameMenuPanel != null) inGameMenuPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (inGameMenuPanel != null) inGameMenuPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

        
    public void OpenInGameMenu()
    {

        isPaused = true;
        Time.timeScale = 0f; 
        if (inGameMenuPanel != null) inGameMenuPanel.SetActive(true);
        SceneManager.LoadScene("Menu_Loja");
    }

    public void CloseInGameMenu()
    {
        isPaused = false;
        Time.timeScale = 1f; 
        if (inGameMenuPanel != null) inGameMenuPanel.SetActive(false);
        if (shopPanel != null) shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        if (shopPanel != null) shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        if (shopPanel != null) shopPanel.SetActive(false);
    }
}
