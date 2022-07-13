using UnityEngine;

public class GameUI : MonoBehaviour {
    private new Audio audio;
    private bool isLoadingScene = false;

    private void Awake() {
        Init();
    }

    private void Init() {
        audio = FindObjectOfType<Audio>();
        audio.PlayGameTrack();
    }

    public void BackToMainMenu() {
        if (isLoadingScene)
            return;
        audio.PlayUISound();
        isLoadingScene = true;
        SceneLoading.Instance.LoadScene("MainMenu");
    }
}