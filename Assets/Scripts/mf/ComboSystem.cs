using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public int ComboCount;
    Text combotext;

    void Start()
    {
        this.combotext = GameObject.Find("ComboText").GetComponent<Text>();
        this.combotext.text = ComboCount.ToString();
    }

    public void AddCombo(int num)
    {
        this.ComboCount += num;
        this.combotext.text =  ComboCount.ToString();
    }
}
