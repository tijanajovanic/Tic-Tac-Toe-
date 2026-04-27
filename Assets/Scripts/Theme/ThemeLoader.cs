using UnityEngine;
using UnityEngine.UI;

public class ThemeLoader : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite[] backgrounds; // 4 pozadine

    [SerializeField] private Sprite[] xSprites; // 4 X sprite-a
    [SerializeField] private Sprite[] oSprites; // 4 O sprite-a
    [SerializeField] private GameObject[] xObjects; // sva X polja
    [SerializeField] private GameObject[] oObjects; // sva O polja

    [SerializeField] private Strike strikeText;
    [SerializeField] private Color[] strikeColors; // 4 boje

    private void Start()
    {
        if (ThemeData.Instance == null) return;

        int theme = ThemeData.Instance.SelectedTheme;

        // MENJANJE POZADINE: Ako u listi postoji slika za tu temu, postavi je na ekran
        if (backgrounds.Length > theme)
            background.sprite = backgrounds[theme];

        // Strike boja
        if (strikeColors.Length > theme && strikeText != null)
            strikeText.GetComponent<TMPro.TextMeshProUGUI>().color = strikeColors[theme];
    }
}