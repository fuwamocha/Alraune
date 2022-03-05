using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxCombo : MonoBehaviour
{
    public static int Count = ComboSystem.maxComboCount;
    private Text _textComponent;

    void Start()
    {
        _textComponent = this.GetComponent<Text>();
        _textComponent.text = Count.ToString();
    }

}
