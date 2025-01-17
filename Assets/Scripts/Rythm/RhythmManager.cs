﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 曲とリズムの同期を行うクラス
/// </summary>
public class RhythmManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] PlayerManager player = default;
    [SerializeField] EnemyManager enemy = default;
    [SerializeField] InputReflector _inputReflector = default;

    public float totalTime { get; private set; }           // トータル経過時間 (sec)

    private int count = 0;                  // BGM切り替え用
    public float elapsedTime { get; private set; }             // 1回毎の経過時間 (sec)
    private float bufferTime;              // 緩衝時間 (タイミングの同期用)
    public float justTime;                // 時間調整用
    private bool cooldown = false;          // 連続実行の防止用


    public float aTime;         // デバッグ用

    private void Start()
    {
        _audioSource = _audioSource.GetComponent<AudioSource>();   // BGMの管理
        _audioSource.clip = BGM[0];
        _audioSource.Play();

        bufferTime = Config.StepSecondsPerBeat * 0.92f;
    }

    private void FixedUpdate()
    {
        totalTime = _audioSource.time;
        elapsedTime = totalTime % Config.StepSecondsPerBeat;

        GetRightTiming();
        _inputReflector.ConvertLocal(elapsedTime);
    }


    /// <summary>
    /// 適正タイミングの算出・実行
    /// </summary>
    private void GetRightTiming()
    {
        if (elapsedTime >= bufferTime)
        {
            if (!cooldown)
            {
                justTime = Config.StepSecondsPerBeat - elapsedTime;
                Invoke("JustTiming", (float)justTime);
                cooldown = true;

                aTime = totalTime + (Config.StepSecondsPerBeat - elapsedTime);     // デバッグ用 (曲の経過時間)
            }
        }
        else if (cooldown)
        {
            cooldown = false;
        }
    }

    private void JustTiming()
    {
        CallAutoJump();

        if (_audioSource.clip == BGM[0])
        {
            count++;
            if (count == 4)
            {
                ChangeBGM();
            }
        }
    }

    private void CallAutoJump()
    {
        player.AutoJump();
        enemy.AutoJump();
    }

    private void ChangeBGM()
    {
        _audioSource.clip = BGM[1];
        _audioSource.Play();
    }
}