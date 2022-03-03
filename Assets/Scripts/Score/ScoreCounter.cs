using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private int perfectScore = 1000;
    [SerializeField] private int successScore = 500;

    [SerializeField] PlayerManager playerManager = default;

    private int totalScore = 0;
    
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StickManager.isPerfect) {
            playerManager.Shan();           //
            totalScore += perfectScore;
            StickManager.isPerfect = false;
        }

        if (StickManager.isSuccess) {
            totalScore += successScore;
            StickManager.isSuccess = false;
        }

        
       scoreText.text = totalScore.ToString("000000");

       if(GameClearMove.isClear){
           PlayerPrefs.SetInt("SCORE", totalScore);
        PlayerPrefs.Save();
       }
    }
}
