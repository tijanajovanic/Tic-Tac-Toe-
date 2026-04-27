using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultOfGame : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI matchDurationText;
    [SerializeField] private MatchTimer matchTimer;
    public MatchTimer GetMatchTimer() => matchTimer;

    private void Start()
    {
        popUpPanel.SetActive(false);

    }

    public void ShowResult(GameRes result, float elapsedTime, SquareState winnerState)
    {
        StartCoroutine(ShowAfterDelay(result, elapsedTime, winnerState));
    }

    private IEnumerator ShowAfterDelay(GameRes result, float elapsedTime, SquareState winnerState)
    {
        float waited = 0f;
        while (waited < 0.6f)
        {
            waited += Time.unscaledDeltaTime;
            yield return null;
        }
        switch (result)
        {
            case GameRes.PlayerWin:
            case GameRes.EnemyWin:
                resultText.text = winnerState == SquareState.x ? "PLAYER 1 WINS!" : "PLAYER 2 WINS!";
                break;
            case GameRes.Draw:
                resultText.text = "DRAW!";
                break;
        }
        int minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);
        matchDurationText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (Statistics.Instance != null)
        {
            Debug.Log("Statistics postoji, salvam...");
            Statistics.Instance.SaveGameResult(result, winnerState, elapsedTime);
        }
        else
        {
            Debug.Log("Statistics.Instance je NULL!");
        }
        if (result == GameRes.Draw) { }
           
        else
            Sound_Manager.Instance.PlayWin();


        popUpPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }

}