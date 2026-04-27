using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager GM;
    public static event System.Action<string> OnMoveMade;
    public static event System.Action OnGameEnded;

    private SquareState PlayerSquareState;
    private SquareState EnemySquareState;
    private Turn CurrentTurn;
    private bool AwaitingInput = false;
    private GameRes CurrentGameRes;

    [SerializeField] private Grid_Manager _Grid_Game_Manager;
    [SerializeField] private Line_Manager _WinLineManager;
    [SerializeField] private Strike _strikeAnim;
    [SerializeField] private ResultOfGame _popUpResult;

    [SerializeField] private TextMeshProUGUI currentTurnText;

    private void Awake()
    {
        if (GM == null)
            GM = this;
        else
            Debug.Log("Error");

        StartNewGame();
        _WinLineManager.HideAllWinLines();

    }

    private void StartNewGame()
    {//resetujem sve parametre na pocetne vrednosti
        CurrentGameRes = GameRes.Ongoing;
        _Grid_Game_Manager.ResetGrid();
        _WinLineManager.HideAllWinLines();

        //  Player 1 je uvek X, Player 2 je uvek O
        PlayerSquareState = SquareState.x;
        EnemySquareState = SquareState.o;

        // Nasumično biramo ko ide PRVI ( simboli ostaju isti)
        int randomStart = Random.Range(0, 2);
        CurrentTurn = (randomStart == 0) ? Turn.PlayerTurn : Turn.EnemyTurn;

        AwaitingInput = true;
        UpdateTurnText();
    }
    private void UpdateTurnText()
    {
        if (currentTurnText == null) return;

        if (CurrentTurn == Turn.PlayerTurn)
        {
            string symbol = PlayerSquareState == SquareState.x ? "X" : "O";
            currentTurnText.text = "PLAYER 1  TURN";
        }
        else
        {
            string symbol = EnemySquareState == SquareState.x ? "X" : "O";
            currentTurnText.text = "PLAYER 2  TURN";
        }
    }

    private void ProcessTurn(Turn turn, int SelectedSquare)
    {
        AwaitingInput = false;
        SquareState state = turn == Turn.PlayerTurn ? PlayerSquareState : EnemySquareState;

        _Grid_Game_Manager.SetThisSquare(state, SelectedSquare); // Upisuje X ili O u mrežicu
        Sound_Manager.Instance.PlaySound();
        OnMoveMade?.Invoke(state == SquareState.x ? "X" : "O");

        bool GameEnd = CheckIfGameEnded();
        if (!GameEnd)
        {
          
            ChangeTurn(); // Menja igrača
            AwaitingInput = true;
            UpdateTurnText(); 
        }
    }

    private bool CheckIfGameEnded()
    {
        bool GridFull = _Grid_Game_Manager.CheckIfGridFull();
        int winLineIndex;
        SquareState Winner = CheckForWin(out winLineIndex);

        if (Winner != SquareState.empty)
        {// POBEDA: Prikazuje liniju, pušta zvuk i otvara PopUp sa rezultatom
            if (currentTurnText != null) currentTurnText.text = "";

            if (Winner == PlayerSquareState)
                CurrentGameRes = GameRes.PlayerWin;
            else
                CurrentGameRes = GameRes.EnemyWin;

            GameRes finalResult = CurrentGameRes;
            float finalTime = _popUpResult.GetMatchTimer().GetElapsedTime();
            SquareState finalWinner = Winner;

            _WinLineManager.ShowWinLine(winLineIndex, () =>
            {
                _strikeAnim.onStrikeComplete = () =>
                {
                    _popUpResult.gameObject.SetActive(true);
                    _popUpResult.ShowResult(finalResult, finalTime, finalWinner);
                };
                Sound_Manager.Instance.PlayStrikeSound();
                _strikeAnim.PlayStrikeAnim();
            });

            OnGameEnded?.Invoke();
            return true;
        }
        else if (GridFull)
        { // NEREŠENO
            if (currentTurnText != null) currentTurnText.text = "";

            CurrentGameRes = GameRes.Draw;
            float finalTime = _popUpResult.GetMatchTimer().GetElapsedTime();
            OnGameEnded?.Invoke();
            _popUpResult.gameObject.SetActive(true);
            _popUpResult.ShowResult(GameRes.Draw, finalTime, SquareState.empty);
            return true;
        }
        else
        {
            return false;
        }
    }

    private SquareState CheckForWin(out int winLineIndex)
    {// kombinacije pobeda
        winLineIndex = -1;
        SquareState Winner = SquareState.empty;

        Winner = _Grid_Game_Manager.CheckForWin(3, 4, 5);
        if (Winner != SquareState.empty) { winLineIndex = 0; return Winner; }

        Winner = _Grid_Game_Manager.CheckForWin(0, 1, 2);
        if (Winner != SquareState.empty) { winLineIndex = 1; return Winner; }

        Winner = _Grid_Game_Manager.CheckForWin(6, 7, 8);
        if (Winner != SquareState.empty) { winLineIndex = 2; return Winner; }

        Winner = _Grid_Game_Manager.CheckForWin(1, 4, 7);
        if (Winner != SquareState.empty) { winLineIndex = 3; return Winner; }

        Winner = _Grid_Game_Manager.CheckForWin(2, 5, 8);
        if (Winner != SquareState.empty) { winLineIndex = 4; return Winner; }

        Winner = _Grid_Game_Manager.CheckForWin(0, 3, 6);
        if (Winner != SquareState.empty) { winLineIndex = 5; return Winner; }

        Winner = _Grid_Game_Manager.CheckForWin(0, 4, 8);
        if (Winner != SquareState.empty) { winLineIndex = 6; return Winner; }

        Winner = _Grid_Game_Manager.CheckForWin(2, 4, 6);
        if (Winner != SquareState.empty) { winLineIndex = 7; return Winner; }

        return SquareState.empty;
    }

    public void ChangeTurn()
    {
        if (CurrentTurn == Turn.PlayerTurn)
            CurrentTurn = Turn.EnemyTurn;
        else
            CurrentTurn = Turn.PlayerTurn;
    }

    public void GridSquareClicked(int ClickedSquare)
    {// Provera da li je polje prazno i da li je dozvoljen klik
        if (AwaitingInput == false) return;
        if (_Grid_Game_Manager.GetThisSquare(ClickedSquare) != SquareState.empty) return;
        ProcessTurn(CurrentTurn, ClickedSquare);
    }
}

public enum Turn { PlayerTurn, EnemyTurn }
public enum GameRes { Ongoing, Draw, PlayerWin, EnemyWin }