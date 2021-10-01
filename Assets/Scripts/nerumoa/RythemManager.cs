using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ȃƃ��Y���̓������s���N���X
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

    private double totalTime = 0d;          // �g�[�^���o�ߎ��� (sec)
    private double elaspedTime;             // 1�񖈂̌o�ߎ��� (sec)
    private double bufferTime;              // �ɏՎ��� (�^�C�~���O�̓����p)
    private double justTime;                // ���Ԓ����p
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;          // �A�����s�̖h�~�p
    private AudioSource audio_BGM;
    //private AudioSource audio_SE;


    public double aTime;        // �f�o�b�O�p

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGM�̊Ǘ�
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();
        //audio_SE = SEs.GetComponent<AudioSource>();     // SE�̊Ǘ�
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
    /// �K���^�C�~���O�̎Z�o�E���s
    /// </summary>
    private void RightTiming()
    {
        if (elaspedTime >= bufferTime) {
            if (!cooldown) {
                justTime = bpm170 - elaspedTime;
                //audio_SE.PlayScheduled(justTime);
                Invoke("CallAutoJump", (float)justTime);
                cooldown = true;

                aTime = totalTime + (bpm170 - elaspedTime);     // �f�o�b�O�p (�Ȃ̌o�ߎ���)
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
