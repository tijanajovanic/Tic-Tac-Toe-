using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
    public void QuitGame()
    {
        // 1. Ako testiraš unutar Unity-ja (Editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // 2. Ako se igra pokrene na itch.io (WebGL)
#elif UNITY_WEBGL
            // Ovde ne možeš ugasiti prozor, pa je najbolje da 
            // otvoriš svoj itch.io profil ili samo restartuješ meni
            Application.OpenURL("https://tvoj-username.itch.io/");

        // 3. Ako je neko skinuo igru na desktop (.exe)
#else
            Application.Quit();
#endif
    }
}
