using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythemManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    [SerializeField] AudioClip[] SE = default;
    [SerializeField] GameObject BGMs = default;
    [SerializeField] GameObject SEs = default;

    private double totalTime = 0d;          // �g�[�^���o�ߎ��� (sec)
    private double elaspedTime;             // 1�񖈂̌o�ߎ��� (sec)
    private double bufferTime;              // �ɏՎ��� (�^�C�~���O�̓����p)
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;          // �A�����s�̖h�~�p

    private AudioSource audio_BGM;
    private AudioSource audio_SE;

    public double aTime;
    public double bTime;

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGM�̊Ǘ�
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();
        audio_SE = SEs.GetComponent<AudioSource>();     // SE�̊Ǘ�
        audio_SE.clip = SE[0];

        bufferTime = bpm170 * 0.92;
    }

    private void FixedUpdate()
    {
        totalTime = audio_BGM.time;
        elaspedTime = totalTime % bpm170;

        /* �K���^�C�~���O�̎Z�o�E���s */
        if (elaspedTime >= bufferTime) {

            if (!cooldown) {
                audio_SE.PlayScheduled(bpm170 - elaspedTime);
                cooldown = true;

                aTime = totalTime + (bpm170 - elaspedTime);     // �f�o�b�O�p (�Ȃ̌o�ߎ���)
                bTime = aTime % bpm170;                         // �f�o�b�O�p
            }
        } else if (cooldown) {
            cooldown = false;
        }
    }
}
