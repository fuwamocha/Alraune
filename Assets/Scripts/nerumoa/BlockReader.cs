using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �u���b�N�̎�ނ�ǂݎ��A�Ή�����������s�킹��N���X
/// </summary>
public class BlockReader : MonoBehaviour
{
    [SerializeField] Player2Manager player = default;

    private double bpm170 = 120 / 170d; // �֐����K�v�H
    private double canSpaceTime;
    private double cantSpaceTime;      // �����Ȃ��u���b�N�p�H
    private double goodStartTime;
    private double goodEndTime;
    private double greatStartTime;
    private double greatEndTime;
    private double exStartTime;
    private double exEndTime;
    private double _elaspedTime = 0d;
    private bool cooldown = true;
    private RaycastHit2D _hit;

    private void Start()
    {
        canSpaceTime   = bpm170 * 0.75;
        cantSpaceTime  = bpm170 * 0.25;      // �����Ȃ��u���b�N�p�H
        goodStartTime  = bpm170 * 0.85;
        goodEndTime    = bpm170 * 0.15;
        greatStartTime = bpm170 * 0.92;
        greatEndTime   = bpm170 * 0.08;
        exStartTime    = bpm170 * 0.97;
        exEndTime      = bpm170 * 0.03;
    }

    private void FixedUpdate()
    {
        ReadBlock();
        PressSpace();
    }


    /// <summary>
    /// �u���b�N�̎�ނ�ǂݎ��
    /// </summary>
    private void ReadBlock()
    {
        if (_hit.collider != null) {

            if (_hit.collider.name == "Tilemap") {

            }
        }
    }

    /// <summary>
    /// �X�y�[�X�ƃ��Y���̓�������
    /// </summary>
    private void PressSpace()
    {
        if (player.pressSpace) {
            player.pressSpace = false;
            if (!cooldown) {
                cooldown = true;
                JudgeSpace();
            }
        }

        if (_elaspedTime > goodEndTime && _elaspedTime <= bpm170 * 0.5 && !cooldown) {
            JudgeSpace();
            cooldown = true;
        } else if (_elaspedTime > bpm170 * 0.5 && _elaspedTime < bpm170 * 0.6 && cooldown) {
            cooldown = false;
        }
    }

    /// <summary>
    /// �X�y�[�X�̃^�C�~���O���菈��
    /// </summary>
    private void JudgeSpace()
    {
        if (_elaspedTime >= exStartTime || _elaspedTime <= exEndTime) {
            Debug.Log("Excellent!");
        } else if (_elaspedTime >= greatStartTime || _elaspedTime <= greatEndTime) {
            Debug.Log("Great!");
        } else if (_elaspedTime >= goodStartTime || _elaspedTime <= goodEndTime) {
            Debug.Log("Good!");
        } else if (_elaspedTime >= canSpaceTime || !cooldown) {
            Debug.Log("Miss!");
        } else {
            Debug.Log("Expectation");
            cooldown = false;
        }
    }

    /// <summary>
    /// BlockReader�N���X�ň����郍�[�J���ϐ��ɕϊ�
    /// </summary>
    public void ConvertLocal(double elaspedTime)
    {
        _elaspedTime = elaspedTime;
        _hit = player.hit;
    }
}
