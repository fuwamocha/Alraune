using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxCombo : MonoBehaviour
{
    Text textComponent;
    public static int Count = ComboSystem.maxComboCount;

    void Start()
    {
        this.textComponent = GameObject.Find("Text").GetComponent<Text>();
        this.textComponent.text = Count.ToString();
    }

}
