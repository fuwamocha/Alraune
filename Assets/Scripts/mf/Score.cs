using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int count = 1;
    Text textComponent;

    void Start()
    {
        this.textComponent = GameObject.Find("ScoreText").GetComponent<Text>();
        this.textComponent.text = score.ToString("000000");
    }

    public void AddScore(int num)
    {
        this.score += (num * count);
        this.textComponent.text = score.ToString("000000");
    }

    private void Update() {
        if(GameClearMove.isClear || EnemyManager.isNotClear){
           PlayerPrefs.SetInt("SCORE", score);
        PlayerPrefs.Save();
       }    
    }
}
