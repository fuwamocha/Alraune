using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScaler : MonoBehaviour
{
    [SerializeField] private RhythmManager _rhythmManager;
    private Vector3 maxScale = 0.5f * Vector3.one;
    private float _pi = (float)Math.PI;
    private float _time;
    private float timef;
    public void Start()
    {
        _rhythmManager = _rhythmManager.GetComponent<RhythmManager>();
    }

    private void Update()
    {

        _time = (float)_rhythmManager.totalTime;
        transform.localScale = Mathf.Sin(2.0f * _pi * Config.StepSecondsPerBeat * _time) * maxScale;
    }
}
