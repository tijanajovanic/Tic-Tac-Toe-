using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    private ToggleSwitch _toggleSwitch;

    private void Awake()
    {
        _toggleSwitch = GetComponent<ToggleSwitch>();
    }

    private void Start()
    {
        // Učitaj sačuvano stanje i postavi toggle vizualno
        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        if (_toggleSwitch != null)
            _toggleSwitch.SetState(musicOn); // postavi vizualno stanje
    }

    public void OnToggleOn()
    {
        if (Sound_Manager.Instance != null)
            Sound_Manager.Instance.PlayClick();
        Sound_Manager.Instance.ToggleMusic(true);
    }

    public void OnToggleOff()
    {
        if (Sound_Manager.Instance != null)
            Sound_Manager.Instance.PlayClick();
        Sound_Manager.Instance.ToggleMusic(false);
    }

}