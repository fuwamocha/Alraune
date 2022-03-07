using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _score;
    private Text _currentScoreText;

    private void Start()
    {
        _score = _score.GetComponent<ScoreCounter>();
        _currentScoreText = this.GetComponent<Text>();
    }

    private void Update()
    {

        _currentScoreText.text = $"{_score.currentScore:000000}";
    }
}
