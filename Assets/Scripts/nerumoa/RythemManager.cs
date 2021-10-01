using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 曲とリズムの同期を行うクラス
/// </summary>
public class RythemManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    //[SerializeField] AudioClip[] SE = default;
    [SerializeField] GameObject BGMs = default;
    //[SerializeField] GameObject SEs = default;
    [SerializeField] Player2Manager player = default;
    [SerializeField] EnemyManager enemy = default;
    [SerializeField] BlockReader block = default;

    private double totalTime = 0d;          // トータル経過時間 (sec)
    private double elaspedTime;             // 1回毎の経過時間 (sec)
    private double bufferTime;              // 緩衝時間 (タイミングの同期用)
    private double justTime;                // 時間調整用
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;          // 連続実行の防止用
    private AudioSource audio_BGM;
    //private AudioSource audio_SE;


    public double aTime;        // デバッグ用

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGMの管理
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();
        //audio_SE = SEs.GetComponent<AudioSource>();     // SEの管理
        //audio_SE.clip = SE[0];

        bufferTime = bpm170 * 0.92;
    }

    private void FixedUpdate()
    {
        totalTime = audio_BGM.time;
        elaspedTime = totalTime % bpm170;

        RightTiming();
        block.ConvertLocal(elaspedTime);
    }


    /// <summary>
    /// 適正タイミングの算出・実行
    /// </summary>
    private void RightTiming()
    {
        if (elaspedTime >= bufferTime) {
            if (!cooldown) {
                justTime = bpm170 - elaspedTime;
                //audio_SE.PlayScheduled(justTime);
                Invoke("CallAutoJump", (float)justTime);
                cooldown = true;

                aTime = totalTime + (bpm170 - elaspedTime);     // デバッグ用 (曲の経過時間)
            }
        } else if (cooldown) {
            cooldown = false;
        }
    }

    private void CallAutoJump()
    {
        player.AutoJump();
        enemy.AutoJump();
    }
}
