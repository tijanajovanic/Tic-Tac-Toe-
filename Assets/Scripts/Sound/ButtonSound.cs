using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        if (Sound_Manager.Instance != null)
            Sound_Manager.Instance.PlayClick();
    }

    public void PlayClickAndLoadScene(string sceneName)
    {
        StartCoroutine(PlaySoundThenLoad(sceneName));
    }

    private IEnumerator PlaySoundThenLoad(string sceneName)
    {
        if (Sound_Manager.Instance != null)
            Sound_Manager.Instance.PlayClick();

        yield return new WaitForSeconds(0.2f); // čekaj da zvuk odsvira

        SceneManager.LoadScene(sceneName);
    }
}