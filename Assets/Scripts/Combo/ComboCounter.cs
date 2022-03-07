using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] ScoreCounter _score;

    public int currentComboCount { get; private set; }
    public int maxComboCount { get; private set; }

    void Start()
    {
        _score = _score.GetComponent<ScoreCounter>();
    }

    public void Update()
    {
        if (currentComboCount >= maxComboCount)
        {
            maxComboCount = currentComboCount;
            PlayerPrefs.SetInt("COMBO", maxComboCount);
        }
    }

    public void MaxCombo()
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

    public void AddCombo(int num)
    {
        currentComboCount += num;
    }

    public void ResetCombo()
    {
        currentComboCount = 0;
    }
}
