using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythemManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    [SerializeField] AudioClip[] SE = default;
    [SerializeField] GameObject BGMs = default;
    [SerializeField] GameObject SEs = default;

    public double totalTime = 0d;          // 合計経過時間
    private double initTime;                // 初期時間
    public double elaspedTime = 0d;             // 経過時間
    private double bufferTime;              // 緩衝時間 (SE用)
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;

    private AudioSource audio_BGM;
    private AudioSource audio_SE;

    public double aTime;

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGMの管理
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();
        audio_SE = SEs.GetComponent<AudioSource>();     // SEの管理
        audio_SE.clip = SE[0];

        initTime = AudioSettings.dspTime;
        bufferTime = bpm170 * 1000d * 0.92;
    }

    private void FixedUpdate()
    {
        totalTime = AudioSettings.dspTime - initTime;   // トータル経過時間 (s)
        elaspedTime = totalTime % bpm170 * 1000d;     // 1回毎の経過時間 (ms)

        if (elaspedTime >= bufferTime) {

            if (!cooldown) {
                audio_SE.PlayScheduled((bpm170 * 1000d - elaspedTime) / 1000d);
                aTime = (bpm170 * 1000d - elaspedTime) / 1000d;
                cooldown = true;
            }
        } else if (cooldown) {
            cooldown = false;
        }
    }
}
