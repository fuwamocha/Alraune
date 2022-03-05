using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;
    private int count = 1;
    private Text textComponent;

    void Start()
    {
        textComponent = this.GetComponent<Text>();
        textComponent.text = score.ToString("000000");
    }

    private void Update()
    {
        // ゲーム終了時に現在スコアを保存
        if (MoveClearScene.isClear || EnemyManager.isNotClear)
        {
            PlayerPrefs.SetInt("SCORE", score);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// 判定に応じてスコア加算
    /// </summary>
    /// <param name="num">スコアの加算値</param>
    public void AddScore(int num)
    {
        this.score += (num * count);
        this.textComponent.text = score.ToString("000000");
    }

}
