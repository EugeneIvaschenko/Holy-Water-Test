using UnityEngine;

public class Audio : MonoBehaviour {
    public static Audio Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip menuTrack;
    [SerializeField] private AudioClip gameTrack;
    [SerializeField] private AudioClip uiClick;

    private void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayMenuTrack() {
        musicSource.clip = menuTrack;
        musicSource.Play();
    }

    public void PlayGameTrack() {
        musicSource.clip = gameTrack;
        musicSource.Play();
    }

    public void PlayUISound() {
        sfxSource.PlayOneShot(uiClick);
    }

    public void SetMusicMute(bool set) {
        musicSource.mute = set;
    }

    public void SetSFXMute(bool set) {
        sfxSource.mute = set;
    }

    public bool IsMusicMute() {
        return musicSource.mute;
    }
    public bool IsSFXMute() {
        return sfxSource.mute;
    }
}