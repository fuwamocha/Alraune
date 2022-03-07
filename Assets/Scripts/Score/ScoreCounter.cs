using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int currentScore { get; private set; }

    private void Update()
    {
        // ゲーム終了時に現在スコアを保存
        if (MoveClearScene.isClear || EnemyManager.isNotClear)
        {
            PlayerPrefs.SetInt("SCORE", currentScore);
            PlayerPrefs.Save();
        }
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
