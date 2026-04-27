using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip resSound;
    [SerializeField] private AudioClip drawSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip strikeSound;


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

    private void Start()
    {
        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        musicSource.mute = !musicOn;
        sfxSource.mute = !sfxOn;
        PlayMenuMusic();
       
        Sound_Manager.Instance.SetMusicVolume(0.5f);

       
        Sound_Manager.Instance.SetSFXVolume(0.8f);
    }

    // MUZIKA
    public void PlayMenuMusic()
    {
        if (menuMusic == null) return;
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayGameMusic()
    {
        if (gameMusic == null) return;
        musicSource.clip = gameMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // SFX
    public void PlayWin()
    {
        if (resSound != null)
            sfxSource.PlayOneShot(resSound);
    }

    public void PlayDraw()
    {
        if (drawSound != null)
            sfxSource.PlayOneShot(drawSound);
    }

    public void PlayClick()
    {
        if (clickSound != null)
            sfxSource.PlayOneShot(clickSound);
    }

   

    public void PlaySound()
    {
        if (placeSound != null)
            sfxSource.PlayOneShot(placeSound);
    }
    public void PlayStrikeSound()
    {
        if (placeSound != null)
            sfxSource.PlayOneShot(strikeSound);
    }

    // VOLUME KONTROLA
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
       
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    
    }

    public void ToggleMusic(bool isOn)
    {
        Debug.Log("ToggleMusic pozvan, isOn: " + isOn);
        musicSource.mute = !isOn;
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSFX(bool isOn)
    {
        Debug.Log("ToggleSFX pozvan, isOn: " + isOn);
        sfxSource.mute = !isOn;
        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}