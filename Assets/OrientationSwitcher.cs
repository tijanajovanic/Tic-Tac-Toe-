using UnityEngine;
using UnityEngine.UI;

public class OrientationSwitcher : MonoBehaviour
{
    [Header("Background")]
    public Sprite portraitSprite;
    public Sprite landscapeSprite;
    private Image backgroundImage;

    [Header("Portrait i Landscape layout")]
    public GameObject portraitLayout;   // prevuci Portrait Canvas/Panel
    public GameObject landscapeLayout;  // prevuci Landscape Canvas/Panel

    void Start()
    {
        backgroundImage = GetComponent<Image>();
        UpdateOrientation();
    }

    void Update()
    {
        UpdateOrientation();
    }

    private void UpdateOrientation()
    {
        bool isLandscape = Screen.width > Screen.height;

        backgroundImage.sprite = isLandscape ? landscapeSprite : portraitSprite;

        if (portraitLayout != null)
            portraitLayout.SetActive(!isLandscape);

        if (landscapeLayout != null)
            landscapeLayout.SetActive(isLandscape);
    }
}