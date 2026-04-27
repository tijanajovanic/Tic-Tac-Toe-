using UnityEngine;
using TMPro;
public class MovesCounter : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI xScoreText;
    public TextMeshProUGUI oScoreText;

    private int xMoves = 0;
    private int oMoves = 0;

    private void OnEnable()
    {
        Game_Manager.OnMoveMade += HandleMoveMade;
    }

    private void OnDisable()
    {
        Game_Manager.OnMoveMade -= HandleMoveMade;
    }

    private void HandleMoveMade(string playerSymbol)
    {
        if (playerSymbol == "X")
        {
            xMoves++;
            xScoreText.text = xMoves.ToString();
        }
        else if (playerSymbol == "O")
        {
            oMoves++;
            oScoreText.text = oMoves.ToString();
        }
    }

    public void ResetCounters()
    {
        xMoves = 0;
        oMoves = 0;
        xScoreText.text = "0";
        oScoreText.text = "0";
    }
}
