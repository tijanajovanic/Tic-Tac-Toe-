using System.Collections;
using UnityEngine;

public class Line_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] _WinLines;

    private static readonly int[,] Lines = new int[,]
    {
        { 0, 1, 2 }, // Horizontale
        { 3, 4, 5 },
        { 6, 7, 8 },
        { 0, 3, 6 },// Vertikale
        { 1, 4, 7 },
        { 2, 5, 8 },
        { 0, 4, 8 },// Dijagonale
        { 2, 4, 6 },
    };

    private void Start() => HideAllWinLines();

    public void ShowWinLine(int lineIndex, System.Action onComplete = null)
    {
        HideAllWinLines();
        if (lineIndex >= 0 && lineIndex < _WinLines.Length)
            StartCoroutine(AnimateLine(_WinLines[lineIndex], onComplete));
    }

    private IEnumerator AnimateLine(GameObject line, System.Action onComplete)
    {
        line.SetActive(true);
        if (Sound_Manager.Instance != null)
            Sound_Manager.Instance.PlayDraw();
        RectTransform rt = line.GetComponent<RectTransform>();
        float duration = 0.35f;
        float time = 0f;

        // Ako je visina veca od sirine = vertikalna linija
        bool isVertical = rt.rect.height > rt.rect.width;
        // Postavi skalu na skoro nulu na početku animacije
        if (isVertical)
            rt.localScale = new Vector3(1f, 0.01f, 1f);
        else
            rt.localScale = new Vector3(0.01f, 1f, 1f);

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / duration;
            t = 1f - Mathf.Pow(1f - t, 2f);
            // Postepeno širi liniju do pune veličine
            if (isVertical)
                rt.localScale = new Vector3(1f, Mathf.Lerp(0.01f, 1f, t), 1f);
            else
                rt.localScale = new Vector3(Mathf.Lerp(0.01f, 1f, t), 1f, 1f);

            yield return null;
        }

        rt.localScale = Vector3.one;
        onComplete?.Invoke();
    }

    public void HideAllWinLines()
    {
        foreach (GameObject line in _WinLines)
            if (line != null) line.SetActive(false);
    }

    public static int GetLength(int dimension) => Lines.GetLength(dimension);
    public static int GetLine(int row, int col) => Lines[row, col];
}