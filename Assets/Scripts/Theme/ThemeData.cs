using UnityEngine;

public class ThemeData : MonoBehaviour
{
    public static ThemeData Instance;
    public int SelectedTheme = 0;

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
}