using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField] ComboCounter _combo;
    private int _maxCombo = 0;
    private int _hiScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        _combo = _combo.GetComponent<ComboCounter>();
        _maxCombo = _combo.maxComboCount;
        _hiScore = PlayerPrefs.GetInt("SCORE");
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(_hiScore);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
