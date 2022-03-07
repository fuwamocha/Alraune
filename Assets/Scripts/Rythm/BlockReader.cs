using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ブロックの種類を読み取り、対応した動作を行わせるクラス
/// </summary>
public class BlockReader : MonoBehaviour
{
    [SerializeField] private GameObject _excellentObj;
    [SerializeField] private GameObject _greatObj;
    [SerializeField] private GameObject _goodObj;
    [SerializeField] private GameObject _missObj;
    [SerializeField] Player2Manager player = default;
    [SerializeField] ComboSystem _comboSystem;
    [SerializeField] Score _score;

    private int timerNum;
    private double bpm170 = 120 / 170d; // 関数が必要？
    private double canSpaceTime;
    private double cantSpaceTime;      // 押さないブロック用？
    private double goodStartTime;
    private double goodEndTime;
    private double greatStartTime;
    private double greatEndTime;
    private double exStartTime;
    private double exEndTime;
    private double _leashedTime = 0d;
    private double bufferTime;
    private double twiceSTime;
    private double twiceETime;
    private double justTime;
    private bool missTime;
    private bool goodTime;
    private bool greatTime;
    private bool exTime;
    private bool pressCooldown = true;
    private bool wrongKey = false;
    private bool pressArrows;
    private bool cooldown;
    private bool cooldown2;
    private string hitBlock;
    private RaycastHit2D _hit;

    public static bool isMiss;

    private enum Block
    {
        NORMAL,
        TWICE,
        SKIP,
        CROSSUP,
        CROSSDOWN,
        CROSSLEFT,
        CROSSRIGHT,
        NONE
    }

    private Block block;

    public GameObject playerpos;

    private void Start()
    {
        _comboSystem = _comboSystem.GetComponent<ComboSystem>();
        _score = _score.GetComponent<Score>();

        pressArrows = player.upArrow || player.downArrow || player.leftArrow || player.rightArrow;
        bufferTime = bpm170 * 0.70;
        twiceSTime = bpm170 * 0.15;
        twiceETime = bpm170 * 0.25;
    }

    private void FixedUpdate()
    {
        CheckTiming();
        PressSpace();
    }


    /// <summary>
    /// ブロックの種類を読み取る
    /// </summary>
    private void ReadBlock()
    {
        if (_hit.collider == null)
        {
            return;
        }

        hitBlock = _hit.collider.name;
        if (hitBlock == "Normal")
        {
            block = Block.NORMAL;
        }
        else if (hitBlock == "Twice")
        {
            block = Block.TWICE;
        }
        else if (hitBlock == "Skip")
        {
            block = Block.SKIP;
        }
        else if (hitBlock == "CrossUp")
        {
            block = Block.CROSSUP;
        }
        else if (hitBlock == "CrossDown")
        {
            block = Block.CROSSDOWN;
        }
        else if (hitBlock == "CrossLeft")
        {
            block = Block.CROSSLEFT;
        }
        else if (hitBlock == "CrossRight")
        {
            block = Block.CROSSRIGHT;
        }
        else if (hitBlock == "None")
        {
            block = Block.NONE;
        }
    }

    /// <summary>
    /// スペースとリズムの同期処理
    /// </summary>
    private void PressSpace()
    {
        switch (block)
        {
            case Block.NORMAL:

                Check(0.4);
                break;

            case Block.TWICE:

                if (timerNum == 1)
                {
                    Check(0.25);
                }
                else if (timerNum == 2)
                {
                    if (canSpaceTime <= _leashedTime && _leashedTime <= goodEndTime)
                    {
                        if (player.pressSpace && !pressCooldown)
                        {
                            pressCooldown = true;
                            JudgeSpace();
                        }
                    }
                    else if (_leashedTime < bpm170 * 0.75 && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                }
                break;

            case Block.SKIP:

                if (canSpaceTime <= _leashedTime || _leashedTime <= cantSpaceTime)
                {

                    if ((player.pressSpace || pressArrows) && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSkip();
                    }
                }
                else if (_leashedTime < bpm170 * 0.4 && !pressCooldown)
                {
                    pressCooldown = true;
                    JudgeSkip();
                }
                break;

            case Block.CROSSUP:

                CheckCross(player.upArrow, player.downArrow, player.leftArrow, player.rightArrow);
                break;

            case Block.CROSSDOWN:

                CheckCross(player.downArrow, player.upArrow, player.leftArrow, player.rightArrow);
                break;

            case Block.CROSSLEFT:

                CheckCross(player.leftArrow, player.upArrow, player.downArrow, player.rightArrow);
                break;

            case Block.CROSSRIGHT:

                CheckCross(player.rightArrow, player.upArrow, player.downArrow, player.leftArrow);
                break;

            case Block.NONE:
                break;
        }

        player.pressSpace = false;
        player.upArrow = false;
        player.downArrow = false;
        player.leftArrow = false;
        player.rightArrow = false;
    }

    private void CheckCross(bool pressArrow, bool otherArrow1, bool otherArrow2, bool otherArrow3)
    {
        if (pressCooldown) return;

        if (canSpaceTime <= _leashedTime || _leashedTime <= goodEndTime)
        {
            if (pressArrow)
            {
                pressCooldown = true;
                JudgeSpace();
            }
            else if (otherArrow1 || otherArrow2 || otherArrow3)
            {
                pressCooldown = true;
                wrongKey = true;
                JudgeSpace();
            }
        }
        else if (_leashedTime < bpm170 * 0.4)
        {
            pressCooldown = true;
            JudgeSpace();
        }
    }

    private void Check(double num)
    {
        if (pressCooldown) return;

        if (canSpaceTime <= _leashedTime || _leashedTime <= goodEndTime)
        {
            if (player.pressSpace)
            {
                pressCooldown = true;
                JudgeSpace();
            }
        }
        else if (_leashedTime < bpm170 * num)
        {
            pressCooldown = true;
            JudgeSpace();
        }
    }

    /// <summary>
    /// スペースのタイミング判定処理
    /// </summary>

    private void JudgeSpace()
    {
        JudgeTime();
        if (missTime)
        {
            Failure();
        }
        else if (exTime)
        {
            Success(_excellentObj, 500);
        }
        else if (greatTime)
        {
            Success(_greatObj, 300);
        }
        else if (goodTime)
        {
            Success(_goodObj, 100);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    private void JudgeSkip()
    {
        missTime = canSpaceTime <= _leashedTime || _leashedTime <= cantSpaceTime;

        if (missTime)
        {
            Failure();
        }
        else
        {
            Success(_excellentObj, 500);
        }
        //audio.Play();
    }

    private void Failure()
    {
        //Debug.Log("Miss!");
        isMiss = true;
        player.MissAnimation();
        player.ReduceHP();
        Instantiate(_missObj, playerpos.transform.position, Quaternion.identity);

        _comboSystem.ComboCount = 0;
        _comboSystem.AddCombo(0);
    }

    private void Success(GameObject timing, int score)
    {
        Instantiate(timing, playerpos.transform.position, Quaternion.identity);

        _score.AddScore(score);
        _comboSystem.AddCombo(1);
    }

    private void JudgeTime()
    {
        if (timerNum == 1)
        {
            exTime = exStartTime <= _leashedTime || _leashedTime <= exEndTime;
            greatTime = greatStartTime <= _leashedTime || _leashedTime <= greatEndTime;
            goodTime = goodStartTime <= _leashedTime || _leashedTime <= goodEndTime;
            missTime = (goodEndTime < _leashedTime && _leashedTime < goodStartTime) || wrongKey;
        }
        else if (timerNum == 2)
        {
            exTime = exStartTime <= _leashedTime && _leashedTime <= exEndTime;
            greatTime = greatStartTime <= _leashedTime && _leashedTime <= greatEndTime;
            goodTime = goodStartTime <= _leashedTime && _leashedTime <= goodEndTime;
            missTime = goodEndTime < _leashedTime || _leashedTime < goodStartTime || wrongKey;
        }
    }

    /// <summary>
    /// BlockReaderクラスで扱えるローカル変数に変換
    /// </summary>
    public void ConvertLocal(double elaspedTime)
    {
        _leashedTime = elaspedTime;
        _hit = player.hit;
    }

    private void NormalTimer()
    {
        timerNum = 1;
        canSpaceTime = bpm170 * 0.72;
        cantSpaceTime = bpm170 * 0.22;      // 押さないブロック用？
        goodStartTime = bpm170 * 0.85;
        goodEndTime = bpm170 * 0.15;
        greatStartTime = bpm170 * 0.92;
        greatEndTime = bpm170 * 0.08;
        exStartTime = bpm170 * 0.97;
        exEndTime = bpm170 * 0.03;
    }

    private void TwiceTimer()
    {
        timerNum = 2;
        canSpaceTime = bpm170 * 0.22;
        cantSpaceTime = bpm170 * 0.72;      // 押さないブロック用？
        goodStartTime = bpm170 * 0.35;
        goodEndTime = bpm170 * 0.65;
        greatStartTime = bpm170 * 0.42;
        greatEndTime = bpm170 * 0.58;
        exStartTime = bpm170 * 0.47;
        exEndTime = bpm170 * 0.53;
    }

    private void CheckTiming()
    {
        /* ブロックの更新 */
        if (_leashedTime >= bufferTime)
        {
            if (!cooldown)
            {
                justTime = bpm170 * 0.75 - _leashedTime;
                Invoke("UpdateBlock", (float)justTime);
                cooldown = true;
            }
        }
        else if (cooldown)
        {
            cooldown = false;
        }

        /* TWICEブロックの更新 */
        if (block == Block.TWICE && twiceSTime <= _leashedTime && _leashedTime <= twiceETime)
        {
            if (!cooldown2)
            {
                justTime = bpm170 * 0.25 - _leashedTime;
                Invoke("UpdateTwice", (float)justTime);
                cooldown2 = true;
            }
        }
        else if (cooldown2)
        {
            cooldown2 = false;
        }
    }
    private void UpdateBlock()
    {
        NormalTimer();
        pressCooldown = false;
        wrongKey = false;
        ReadBlock();
    }

    private void UpdateTwice()
    {
        TwiceTimer();
        pressCooldown = false;
        wrongKey = false;
    }
}