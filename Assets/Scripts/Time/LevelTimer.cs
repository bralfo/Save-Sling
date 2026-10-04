using System.Collections;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    [SerializeField] private float timeRemaining = 60f; // Tempo da fase em segundos (ex: 60s)
    private bool timerIsRunning = false;

    [Header("Referências da Cena")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private MenuController menuController;
    [SerializeField] private Wallet wallet;

    private void Start()
    {
        // Inicia o timer
        timerIsRunning = true;
        UpdateTimerDisplay(timeRemaining);
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(timeRemaining);

                
                OnTimerEnd();
            }
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timerText == null) return;

        
        if (timeToDisplay < 0) timeToDisplay = 0;

        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimerEnd()
    {
        // 
        if (wallet != null)
        {
            int currentSavedGold = PlayerPrefs.GetInt("TotalGold", 0);
            int updatedTotal = currentSavedGold + wallet.CurrentGold;

            PlayerPrefs.SetInt("TotalGold", updatedTotal);
            PlayerPrefs.Save(); 

            Debug.Log($"Tempo esgotado! Ouro salvo nesta rodada: {wallet.CurrentGold}. Ouro total acumulado: {updatedTotal}");
        }
        else
        {
            Debug.LogWarning("Wallet não associada no LevelTimer! O valor do ouro não pôde ser salvo.");
        }

        
        if (menuController != null)
        {
            menuController.TriggerGameOver();
        }
        else
        {
            Debug.LogError("MenuController não foi associado no Inspector do LevelTimer!");
        }
    }
}
