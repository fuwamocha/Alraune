using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public int ComboCount;
    public bool call = false;
    public int maxComboCount;

    Text combotext;

    void Start()
    {
        this.combotext = GameObject.Find("ComboText").GetComponent<Text>();
        this.combotext.text = ComboCount.ToString();
    }

    public void Update()
    {
        if (ComboCount >= maxComboCount)
        {
            maxComboCount = ComboCount;
        }
    }

    public void MaxCombo()
    {
        switch (maxComboCount)
        {
            case 50:
                GameObject.Find("ScoreText").GetComponent<Score>().AddScore(500);
                break;
            case 100:
                GameObject.Find("ScoreText").GetComponent<Score>().AddScore(1000);
                break;
            case 200:
                GameObject.Find("ScoreText").GetComponent<Score>().AddScore(2000);
                break;
        }
    }

    public void AddCombo(int num)
    {
        this.ComboCount += num;
        this.combotext.text = ComboCount.ToString();
    }
}
