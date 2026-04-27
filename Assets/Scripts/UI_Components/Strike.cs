using System.Collections;
using UnityEngine;
using TMPro;

public class Strike : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [SerializeField] private float displayDuration = 1.5f;
    [SerializeField] private float fadeDuration = 0.6f; // Izvučeno kao varijabla radi lakše promene

    public System.Action onStrikeComplete;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.raycastTarget = false;

        // Osiguraj da je na početku nevidljiv i isključen
        _text.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void PlayStrikeAnim()
    {
        // Ako je neko pozvao animaciju, prvo aktiviramo objekat
        gameObject.SetActive(true);
        StopAllCoroutines(); // Sprečava bagove ako se animacija pokrene dvaput brzo
        StartCoroutine(StrikeSequence());
    }

    private IEnumerator StrikeSequence()
    {
        // Fade in
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            _text.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }
        _text.alpha = 1f;

        // Čekanje
        float waited = 0f;
        while (waited < displayDuration)
        {
            waited += Time.unscaledDeltaTime;
            yield return null;
        }

        // Fade out
        time = 0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            _text.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }
        _text.alpha = 0f;

        // Pozivamo event i GASIMO ceo objekat da ne smeta na ekranu
        onStrikeComplete?.Invoke();
        gameObject.SetActive(false);
    }
}