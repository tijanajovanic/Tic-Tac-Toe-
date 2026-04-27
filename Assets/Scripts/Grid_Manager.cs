using UnityEngine;

public class Grid_Manager : MonoBehaviour
{
    [SerializeField] private Square_Manager[] _Grid_Manager;

    private void Awake()
    {
        Debug.Log("Resetujem grid");
        ResetGrid();

    }
    public void ResetGrid()
    {
        foreach (Square_Manager square in _Grid_Manager)
        {
            square.SetSquare(SquareState.empty);
        }
        for (int i = 0; i < _Grid_Manager.Length; i++)
        {
            _Grid_Manager[i].SetSquare(SquareState.empty);
            _Grid_Manager[i].SetSquareID(i);
        }
    }
    public void SetThisSquare(SquareState GridSquareState, int Square)
    {
        _Grid_Manager[Square].SetSquare(GridSquareState);

    }
    public SquareState GetThisSquare(int squareID)
    {
        return _Grid_Manager[squareID].GetSquareState();
    }

    public bool CheckIfGridFull()
    {
        foreach (Square_Manager square in _Grid_Manager)
        {
            if (square.GetSquareState() == SquareState.empty)
            {
                return false;
            }
        }
        return true;
    }
    public SquareState CheckForWin(int GridSquare1, int GridSquare2, int GridSquare3)
    {
        SquareState state1 = _Grid_Manager[GridSquare1].GetSquareState();
        SquareState state2 = _Grid_Manager[GridSquare2].GetSquareState();
        SquareState state3 = _Grid_Manager[GridSquare3].GetSquareState();

        if (state2 != SquareState.empty)
        {
            if (state1 == state2 && state1 == state3)
            {
                return state1;
            }
            else
            {
                return SquareState.empty;
            }

        }
        return SquareState.empty;
    }
}
