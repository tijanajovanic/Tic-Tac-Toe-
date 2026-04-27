using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalGamesText;
    [SerializeField] private TextMeshProUGUI player1WinsText;
    [SerializeField] private TextMeshProUGUI player2WinsText;
    [SerializeField] private TextMeshProUGUI drawsText;
    [SerializeField] private TextMeshProUGUI avgDurationText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (Statistics.Instance == null) return;

        totalGamesText.text = Statistics.Instance.GetTotalGames().ToString();
        player1WinsText.text = Statistics.Instance.GetPlayer1Wins().ToString();
        player2WinsText.text = Statistics.Instance.GetPlayer2Wins().ToString();
        drawsText.text = Statistics.Instance.GetDraws().ToString();

        float avg = Statistics.Instance.GetAverageDuration();
        int minutes = (int)(avg / 60f);
        int seconds = (int)(avg % 60f);
        avgDurationText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetStats()
    {
        Statistics.Instance.ResetStats();
        UpdateUI();
    }
}