using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの種類を読み取り、対応した動作を行わせるクラス
/// </summary>
public class BlockReader : MonoBehaviour
{
    [SerializeField] Player2Manager player = default;

    private double bpm170 = 120 / 170d; // 関数が必要？
    private double canSpaceTime;
    private double cantSpaceTime;      // 押さないブロック用？
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
        cantSpaceTime  = bpm170 * 0.25;      // 押さないブロック用？
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
    /// ブロックの種類を読み取る
    /// </summary>
    private void ReadBlock()
    {
        if (_hit.collider != null) {

            if (_hit.collider.name == "Tilemap") {

            }
        }
    }

    /// <summary>
    /// スペースとリズムの同期処理
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
    /// スペースのタイミング判定処理
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
    /// BlockReaderクラスで扱えるローカル変数に変換
    /// </summary>
    public void ConvertLocal(double elaspedTime)
    {
        _elaspedTime = elaspedTime;
        _hit = player.hit;
    }
}
