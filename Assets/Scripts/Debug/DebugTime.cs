using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTime : MonoBehaviour
{
    [SerializeField] RhythmManager rhythm = default;
    [SerializeField] Text timeText = default;

    public bool isChecked;

    private void Update()
    {
        if (isChecked)
        {
            timeText.text = rhythm.aTime.ToString("N9");
            timeText.text += "sec";
        }
    }

    public void Flush()
    {
        timeText.text = rhythm.aTime.ToString("N9");
        timeText.text += "sec";
    }
}
