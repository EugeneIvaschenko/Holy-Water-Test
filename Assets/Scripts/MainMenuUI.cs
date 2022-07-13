using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private RectTransform mainDisplay;
    [SerializeField] private RectTransform settingsDisplay;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private float menuMovingDuration = 0.3f;
    private new Audio audio;

    private bool isInteractible = true;

    private void Awake() {
        UIInit();
        AudioInit();
    }

    private void UIInit() {
        mainDisplay.gameObject.SetActive(true);
        settingsDisplay.gameObject.SetActive(false);
        settingsDisplay.transform.position = new(Screen.width + settingsDisplay.rect.width / 2, settingsDisplay.transform.position.y, settingsDisplay.transform.position.z);
    }

    private void AudioInit() {
        audio = FindObjectOfType<Audio>();
        audio.PlayMenuTrack();
        musicToggle.SetIsOnWithoutNotify(!audio.IsMusicMute());
        sfxToggle.SetIsOnWithoutNotify(!audio.IsSFXMute());
    }

    public void StartGame() {
        if (!isInteractible)
            return;
        audio.PlayUISound();
        BlockButtons();
        SceneLoading.Instance.LoadScene("GameScene");
    }

    public void OpenSettings() {
        if (!isInteractible)
            return;
        audio.PlayUISound();
        BlockButtons();
        settingsDisplay.gameObject.SetActive(true);
        settingsDisplay.transform.DOMove(mainDisplay.position, menuMovingDuration).OnComplete(() => {
            UnblockButtons();
            mainDisplay.gameObject.SetActive(false);
        });
        mainDisplay.transform.DOMoveX(-Screen.width / 2, menuMovingDuration);
    }

    public void BackToMain() {
        if (!isInteractible)
            return;
        audio.PlayUISound();
        BlockButtons();
        mainDisplay.gameObject.SetActive(true);
        mainDisplay.transform.DOMove(settingsDisplay.position, menuMovingDuration).OnComplete(() => {
            UnblockButtons();
            settingsDisplay.gameObject.SetActive(false);
        });
        settingsDisplay.transform.DOMoveX(Screen.width + settingsDisplay.rect.width / 2, menuMovingDuration);
    }

    public void SwitchMusic() {
        audio.SetMusicMute(!musicToggle.isOn);
        audio.PlayUISound();
    }

    public void SwitchSFX() {
        audio.SetSFXMute(!sfxToggle.isOn);
        audio.PlayUISound();
    }

    private void UnblockButtons() => isInteractible = true;

    private void BlockButtons() => isInteractible = false;
}