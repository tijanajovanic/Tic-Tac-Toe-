using TMPro;
using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public float GetElapsedTime() => elapsedTime;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    private void Start()
    {
        isRunning = true; // Krece cim se scena učita
    }

    private void OnEnable()
    {
       
        Game_Manager.OnGameEnded += StopTimer;
    }

    private void OnDisable()
    {
     
        Game_Manager.OnGameEnded -= StopTimer;
    }

   

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = false;
        timerText.text = "00:00";
    }
}
