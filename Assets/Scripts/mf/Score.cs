using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;
    Text textComponent;

    void Start()
    {
        this.textComponent = GameObject.Find("ScoreText").GetComponent<Text>();
        this.textComponent.text = score.ToString();
    }

    public void AddScore(int num)
    {
        this.score += num;
        this.textComponent.text = score.ToString();
    }
}
