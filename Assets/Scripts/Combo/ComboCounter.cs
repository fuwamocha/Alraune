using UniRx;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] private ClearStatusNotifier _clearStatusNotifier;
    [SerializeField] ScoreCounter _score;

    public int currentComboCount { get; private set; }
    public int maxComboCount { get; private set; }

    private void Start()
    {
        _clearStatusNotifier
            .ObserveEveryValueChanged(x => x.isGameClear || x.isGameOver)
            .Where(x => x)
            .Subscribe(_ => SetCombo())
            .AddTo(this);
    }

    private void SetCombo()
    {
        PlayerPrefs.SetInt("COMBO", maxComboCount);
        PlayerPrefs.Save();
    }
    public void AddCombo(int num)
    {
        currentComboCount += num;

        ComboBonus();

        // 最大コンボ数の更新
        if (currentComboCount >= maxComboCount) maxComboCount = currentComboCount;
    }

    public void ResetCombo()
    {
        currentComboCount = 0;
    }

    public void ComboBonus()
    {
        switch (maxComboCount)
        {
            case int x when x == 50:
                _score.AddScore(x * 10);
                break;
            case int x when x == 100:
                _score.AddScore(x * 10);
                break;
            case int x when x == 200:
                _score.AddScore(x * 10);
                break;
        }
    }
}
