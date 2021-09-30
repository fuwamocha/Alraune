using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public int ComboCount;
    public bool call = false;
    Text combotext;

    void Start()
    {
        this.combotext = GameObject.Find("ComboText").GetComponent<Text>();
        this.combotext.text = ComboCount.ToString();
    }

    public void FixedUpdate()
    {
        if (ComboCount == 5 && call == false)
        {
            call = true;
            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(5000);
            StartCoroutine("Delay");
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        call = false;
    }

    public void AddCombo(int num)
    {
        this.ComboCount += num;
        this.combotext.text = ComboCount.ToString();
    }
}
