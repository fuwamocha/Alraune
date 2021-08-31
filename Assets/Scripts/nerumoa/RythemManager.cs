using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythemManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    [SerializeField] AudioClip[] SE = default;
    [SerializeField] GameObject BGMs = default;
    [SerializeField] GameObject SEs = default;

    public double totalTime = 0d;          // ���v�o�ߎ���
    private double initTime;                // ��������
    public double elaspedTime = 0d;             // �o�ߎ���
    private double bufferTime;              // �ɏՎ��� (SE�p)
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;

    private AudioSource audio_BGM;
    private AudioSource audio_SE;

    public double aTime;

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGM�̊Ǘ�
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();
        audio_SE = SEs.GetComponent<AudioSource>();     // SE�̊Ǘ�
        audio_SE.clip = SE[0];

        initTime = AudioSettings.dspTime;
        bufferTime = bpm170 * 1000d * 0.92;
    }

    private void FixedUpdate()
    {
        totalTime = AudioSettings.dspTime - initTime;   // �g�[�^���o�ߎ��� (s)
        elaspedTime = totalTime % bpm170 * 1000d;     // 1�񖈂̌o�ߎ��� (ms)

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
