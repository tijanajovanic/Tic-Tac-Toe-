using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Square_Manager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject xObject;
    [SerializeField] private GameObject oObject;

    [SerializeField] private Sprite[] xSprites; // 4 X sprite-a
    [SerializeField] private Sprite[] oSprites; // 4 O sprite-a

    private int _SquareID;
    private SquareState CurrentState = SquareState.empty;

    private void Start()
    {// Na samom početku, proveri koja je tema izabrana i postavi odgovarajuće slike
        if (ThemeData.Instance != null)
        {
            int theme = ThemeData.Instance.SelectedTheme;
            if (xSprites.Length > theme)
                xObject.GetComponent<Image>().sprite = xSprites[theme];
            if (oSprites.Length > theme)
                oObject.GetComponent<Image>().sprite = oSprites[theme];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Game_Manager.GM.GridSquareClicked(_SquareID);
    }
    // Vraća informaciju da li je polje prazno, X ili O
    public SquareState GetSquareState()
    {
        return CurrentState;
    }

    public void SetSquare(SquareState NewState)
    {
        if (NewState == SquareState.empty)
        {
            xObject.SetActive(false);
            oObject.SetActive(false);
        }
        else if (NewState == SquareState.x)
        {
            xObject.SetActive(true);
            oObject.SetActive(false);
        }
        else if (NewState == SquareState.o)
        {
            oObject.SetActive(true);
            xObject.SetActive(false);
        }
        CurrentState = NewState;
    }

    public void SetSquareID(int ID)
    {
        _SquareID = ID;
    }// Funkcija koju poziva ThemeLoader da bi rekao kvadratu koju sliku da koristi
    public void UpdateThemeSprites(Sprite newX, Sprite newO)
    {
        if (xObject != null) xObject.GetComponent<Image>().sprite = newX;
        if (oObject != null) oObject.GetComponent<Image>().sprite = newO;
    }
}

public enum SquareState { empty, x, o };
