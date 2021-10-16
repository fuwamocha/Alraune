using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ȃƃ��Y���̓������s���N���X
/// </summary>
public class RythemManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    [SerializeField] GameObject BGMs = default;
    [SerializeField] Player2Manager player = default;
    [SerializeField] EnemyManager enemy = default;
    [SerializeField] BlockReader block = default;

    public double totalTime = 0d;           // �g�[�^���o�ߎ��� (sec)

    private int count = 0;                  // BGM�؂�ւ��p
    private double elaspedTime;             // 1�񖈂̌o�ߎ��� (sec)
    private double bufferTime;              // �ɏՎ��� (�^�C�~���O�̓����p)
    private double justTime;                // ���Ԓ����p
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;          // �A�����s�̖h�~�p
    private AudioSource audio_BGM;

    public double aTime;        // �f�o�b�O�p

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGM�̊Ǘ�
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();

        bufferTime = bpm170 * 0.92;
    }

    private void FixedUpdate()
    {
        totalTime = audio_BGM.time;
        elaspedTime = totalTime % bpm170;

        GetRightTiming();
        block.ConvertLocal(elaspedTime);
    }


    /// <summary>
    /// �K���^�C�~���O�̎Z�o�E���s
    /// </summary>
    private void GetRightTiming()
    {
        if (elaspedTime >= bufferTime) {
            if (!cooldown) {
                justTime = bpm170 - elaspedTime;
                Invoke("JustTiming", (float)justTime);
                cooldown = true;

                aTime = totalTime + (bpm170 - elaspedTime);     // �f�o�b�O�p (�Ȃ̌o�ߎ���)
            }
        } else if (cooldown) {
            cooldown = false;
        }
    }

    private void JustTiming()
    {
        CallAutoJump();

        if (audio_BGM.clip == BGM[0]) {
            count++;
            if (count == 4) {
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
        audio_BGM.clip = BGM[1];
        audio_BGM.Play();
    }
}