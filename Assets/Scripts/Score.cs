using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textField;
    private int score = 0;

    private void Awake() {
        UpdateText();
    }

    public void AddPoint() {
        score++;
        UpdateText();
    }

    private void UpdateText() {
        if (textField)
            textField.text = score.ToString();
    }
}