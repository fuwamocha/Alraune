using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTime : MonoBehaviour
{
    [SerializeField] RythemManager rythem = default;
    [SerializeField] Text timeText = default;

    public bool isChecked;

    private void Update()
    {
        if (isChecked) {
            timeText.text = rythem.aTime.ToString("N9");
        } else {
            timeText.text = rythem.bTime.ToString("N9");
        }
    }
}
