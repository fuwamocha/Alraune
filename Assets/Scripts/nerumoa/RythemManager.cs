using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythemManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    [SerializeField] AudioClip[] SE = default;
    [SerializeField] GameObject BGMs = default;
    [SerializeField] GameObject SEs = default;

    private double totalTime = 0d;          // トータル経過時間 (sec)
    private double elaspedTime;             // 1回毎の経過時間 (sec)
    private double bufferTime;              // 緩衝時間 (タイミングの同期用)
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;          // 連続実行の防止用

    private AudioSource audio_BGM;
    private AudioSource audio_SE;

    public double aTime;
    public double bTime;

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGMの管理
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();
        audio_SE = SEs.GetComponent<AudioSource>();     // SEの管理
        audio_SE.clip = SE[0];

        bufferTime = bpm170 * 0.92;
    }

    private void FixedUpdate()
    {
        totalTime = audio_BGM.time;
        elaspedTime = totalTime % bpm170;

        /* 適正タイミングの算出・実行 */
        if (elaspedTime >= bufferTime) {

            if (!cooldown) {
                audio_SE.PlayScheduled(bpm170 - elaspedTime);
                cooldown = true;

                aTime = totalTime + (bpm170 - elaspedTime);     // デバッグ用 (曲の経過時間)
                bTime = aTime % bpm170;                         // デバッグ用
            }
        } else if (cooldown) {
            cooldown = false;
        }
    }
}
