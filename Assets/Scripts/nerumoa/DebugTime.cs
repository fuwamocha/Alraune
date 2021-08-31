using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTime : MonoBehaviour
{
    [SerializeField] RythemManager rythem = default;
    [SerializeField] Text timeText = default;

    private int sec;
    private float dec;
    private float totalTime = 0f;

    private bool isCount = true;

    public bool isChecked;

    private void Update()
    {
        /*
        if (isCount) {
            totalTime += Time.deltaTime;
            timeText.text = totalTime.ToString("N2");
        }
        */

        if (isChecked) {
            timeText.text = rythem.elaspedTime.ToString("N2");
        } else {
            timeText.text = rythem.aTime.ToString("N4");
        }
    }

    public void CountStop()
    {
        isCount = false;
    }
}
