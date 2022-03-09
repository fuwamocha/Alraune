using UniRx;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ClearStatusNotifier _clearStatusNotifier;
    public int currentScore { get; private set; }

    private void Start()
    {
        _clearStatusNotifier
            .ObserveEveryValueChanged(x => x.isGameClear || x.isGameOver)
            .Where(x => x)
            .Subscribe(_ => SetScore())
            .AddTo(this);
    }

    private void SetScore()
    {
        PlayerPrefs.SetInt("SCORE", currentScore);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 判定に応じてスコア加算
    /// </summary>
    /// <param name="num">スコアの加算値</param>
    public void AddScore(int num)
    {
        this.currentScore += num;
    }

}
