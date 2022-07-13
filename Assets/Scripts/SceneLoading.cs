using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour {
    public static SceneLoading Instance { get; private set; }

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider progressBar;

    private void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            loadingScreen.SetActive(false);
        } else {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName) {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        do {
            await Task.Delay(10);
            progressBar.value = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(100);

        scene.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }
}