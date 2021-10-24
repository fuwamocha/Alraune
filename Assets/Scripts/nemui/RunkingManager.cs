using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunkingManager : MonoBehaviour
{
    private int _maxCombo = 0;
    private int _hiScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        _maxCombo = MaxCombo.Count;
        _hiScore = PlayerPrefs.GetInt("SCORE");
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (_hiScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
