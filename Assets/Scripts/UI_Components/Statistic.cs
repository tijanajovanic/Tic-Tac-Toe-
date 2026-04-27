using UnityEngine;
public class Statistics : MonoBehaviour
{
    private const string TOTAL_GAMES = "TotalGames";
    private const string PLAYER1_WINS = "Player1Wins";
    private const string PLAYER2_WINS = "Player2Wins";
    private const string DRAWS = "Draws";
    private const string TOTAL_DURATION = "TotalDuration";
    public static Statistics Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveGameResult(GameRes result, SquareState winnerState, float duration)
    {// Uzima trenutni broj partija, dodaje 1 i odmah ih cuva
        int totalGames = PlayerPrefs.GetInt(TOTAL_GAMES, 0) + 1;
        PlayerPrefs.SetInt(TOTAL_GAMES, totalGames);
        // Provera ko je pobedio i ažuriranje odgovarajućeg brojača
        if (result == GameRes.PlayerWin || result == GameRes.EnemyWin)
        {
            if (winnerState == SquareState.x)
            {
                int p1wins = PlayerPrefs.GetInt(PLAYER1_WINS, 0) + 1;
                PlayerPrefs.SetInt(PLAYER1_WINS, p1wins);
            }
            else
            {
                int p2wins = PlayerPrefs.GetInt(PLAYER2_WINS, 0) + 1;
                PlayerPrefs.SetInt(PLAYER2_WINS, p2wins);
            }
        }
        else if (result == GameRes.Draw)
        {
            int draws = PlayerPrefs.GetInt(DRAWS, 0) + 1;
            PlayerPrefs.SetInt(DRAWS, draws);
        }
        // Sabira vreme trajanja svih partija radi proseka
        float totalDuration = PlayerPrefs.GetFloat(TOTAL_DURATION, 0f) + duration;
        Debug.Log("Saving game #" + totalGames + " duration: " + duration);
        PlayerPrefs.SetFloat(TOTAL_DURATION, totalDuration);
        PlayerPrefs.Save();
    }// Funkcije za čitanje podataka koje se prikazuju u UI-ju
    public int GetTotalGames() => PlayerPrefs.GetInt(TOTAL_GAMES, 0);
    public int GetPlayer1Wins() => PlayerPrefs.GetInt(PLAYER1_WINS, 0);
    public int GetPlayer2Wins() => PlayerPrefs.GetInt(PLAYER2_WINS, 0);
    public int GetDraws() => PlayerPrefs.GetInt(DRAWS, 0);
    public float GetAverageDuration()
    {
        int total = GetTotalGames();
        if (total == 0) return 0f;
        return PlayerPrefs.GetFloat(TOTAL_DURATION, 0f) / total;
    }
    public void ResetStats()
    {
        PlayerPrefs.DeleteKey(TOTAL_GAMES);
        PlayerPrefs.DeleteKey(PLAYER1_WINS);
        PlayerPrefs.DeleteKey(PLAYER2_WINS);
        PlayerPrefs.DeleteKey(DRAWS);
        PlayerPrefs.DeleteKey(TOTAL_DURATION);
        PlayerPrefs.Save();
    }
}