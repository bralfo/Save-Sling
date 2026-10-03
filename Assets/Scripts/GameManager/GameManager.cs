using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;

    [Header("Game State")]
    public bool partidaAcabou = false;
    [SerializeField] private float tempoPartida = 180f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempoPartida -= Time.deltaTime;

        if (tempoPartida <= 0)
        {
            tempoPartida = 0;
            partidaAcabou = true;
        }
        int minutos = Mathf.FloorToInt(tempoPartida / 60);
        int segundos = Mathf.FloorToInt(tempoPartida % 60);

        timerText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}
