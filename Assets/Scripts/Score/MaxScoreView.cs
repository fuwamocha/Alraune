using UnityEngine;
using UnityEngine.UI;

public class MaxScoreView : MonoBehaviour
{
    private Text _scoreText;

    private void Start()
    {
        _scoreText = this.GetComponent<Text>();

        var resultScore = PlayerPrefs.GetInt("SCORE");
        _scoreText.text = $"{resultScore:000000}";
    }
}
