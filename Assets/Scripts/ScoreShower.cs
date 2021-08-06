using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShower : MonoBehaviour
{
    private int resultScore;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        resultScore = PlayerPrefs.GetInt("SCORE");

        scoreText.text = resultScore.ToString("000000");
        Debug.Log("得点：" + resultScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
