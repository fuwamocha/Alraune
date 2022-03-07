using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ブロックの種類を読み取り、対応した動作を行わせるクラス
/// </summary>
public class BlockReader : MonoBehaviour
{

    [SerializeField] Player2Manager player = default;

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
    private double _elaspedTime = 0d;
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
    private bool pressArrow;
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

    public GameObject excellent;
    public GameObject great;
    public GameObject Good;
    public GameObject Miss;

    private void Start()
    {
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

                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime)
                {
                    if (player.pressSpace && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                }
                else if (_elaspedTime < bpm170 * 0.4 && !pressCooldown)
                {
                    pressCooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.TWICE:

                if (timerNum == 1)
                {
                    if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime)
                    {
                        if (player.pressSpace && !pressCooldown)
                        {
                            pressCooldown = true;
                            JudgeSpace();
                        }
                    }
                    else if (_elaspedTime < bpm170 * 0.25 && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                }
                else if (timerNum == 2)
                {
                    if (canSpaceTime <= _elaspedTime && _elaspedTime <= goodEndTime)
                    {
                        if (player.pressSpace && !pressCooldown)
                        {
                            pressCooldown = true;
                            JudgeSpace();
                        }
                    }
                    else if (_elaspedTime < bpm170 * 0.75 && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                }
                break;

            case Block.SKIP:

                if (canSpaceTime <= _elaspedTime || _elaspedTime <= cantSpaceTime)
                {
                    pressArrow = player.upArrow || player.downArrow || player.leftArrow || player.rightArrow;
                    if ((player.pressSpace || pressArrow) && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSkip();
                    }
                }
                else if (_elaspedTime < bpm170 * 0.4 && !pressCooldown)
                {
                    pressCooldown = true;
                    JudgeSkip();
                }
                break;

            case Block.CROSSUP:

                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime)
                {
                    if (player.upArrow && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                    else if (player.downArrow || player.leftArrow || player.rightArrow)
                    {
                        pressCooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                }
                else if (_elaspedTime < bpm170 * 0.4 && !pressCooldown)
                {
                    pressCooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.CROSSDOWN:

                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime)
                {
                    if (player.downArrow && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                    else if (player.upArrow || player.leftArrow || player.rightArrow)
                    {
                        pressCooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                }
                else if (_elaspedTime < bpm170 * 0.4 && !pressCooldown)
                {
                    pressCooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.CROSSLEFT:

                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime)
                {
                    if (player.leftArrow && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                    else if (player.upArrow || player.downArrow || player.rightArrow)
                    {
                        pressCooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                }
                else if (_elaspedTime < bpm170 * 0.4 && !pressCooldown)
                {
                    pressCooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.CROSSRIGHT:

                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime)
                {
                    if (player.rightArrow && !pressCooldown)
                    {
                        pressCooldown = true;
                        JudgeSpace();
                    }
                    else if (player.upArrow || player.downArrow || player.leftArrow)
                    {
                        pressCooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                }
                else if (_elaspedTime < bpm170 * 0.4 && !pressCooldown)
                {
                    pressCooldown = true;
                    JudgeSpace();
                }
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

    /// <summary>
    /// スペースのタイミング判定処理
    /// </summary>
    private void JudgeSpace()
    {
        JudgeTime();
        if (missTime)
        {
            //Debug.Log("Miss!");
            isMiss = true;
            player.MissAnimation();
            player.ReduceHP();
            Instantiate(Miss, playerpos.transform.position, Quaternion.identity);

            GameObject.Find("ComboText").GetComponent<ComboSystem>().ComboCount = 0;
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(0);
            return;
        }
        else if (exTime)
        {
            //Debug.Log("Excellent!");
            Instantiate(excellent, playerpos.transform.position, Quaternion.identity);

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(500);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        }
        else if (greatTime)
        {
            //Debug.Log("Great!");
            Instantiate(great, playerpos.transform.position, Quaternion.identity);

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(300);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        }
        else if (goodTime)
        {
            //Debug.Log("Good!");
            Instantiate(Good, playerpos.transform.position, Quaternion.identity);

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(100);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        }
        else
        {
            Debug.Log("ERROR");
            return;
        }
    }

    private void JudgeSkip()
    {
        missTime = canSpaceTime <= _elaspedTime || _elaspedTime <= cantSpaceTime;

        if (missTime)
        {
            //Debug.Log("Miss!");
            isMiss = true;
            player.MissAnimation();
            player.ReduceHP();
            Instantiate(Miss, playerpos.transform.position, Quaternion.identity);

            GameObject.Find("ComboText").GetComponent<ComboSystem>().ComboCount = 0;
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(0);
            return;
        }
        else
        {
            //Debug.Log("Excellent!");
            Instantiate(excellent, playerpos.transform.position, Quaternion.identity);

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(500);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        }

        //audio.Play();
    }

    private void JudgeTime()
    {
        if (timerNum == 1)
        {
            exTime = exStartTime <= _elaspedTime || _elaspedTime <= exEndTime;
            greatTime = greatStartTime <= _elaspedTime || _elaspedTime <= greatEndTime;
            goodTime = goodStartTime <= _elaspedTime || _elaspedTime <= goodEndTime;
            missTime = (goodEndTime < _elaspedTime && _elaspedTime < goodStartTime) || wrongKey;
        }
        else if (timerNum == 2)
        {
            exTime = exStartTime <= _elaspedTime && _elaspedTime <= exEndTime;
            greatTime = greatStartTime <= _elaspedTime && _elaspedTime <= greatEndTime;
            goodTime = goodStartTime <= _elaspedTime && _elaspedTime <= goodEndTime;
            missTime = goodEndTime < _elaspedTime || _elaspedTime < goodStartTime || wrongKey;
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
        if (_elaspedTime >= bufferTime)
        {
            if (!cooldown)
            {
                justTime = bpm170 * 0.75 - _elaspedTime;
                Invoke("UpdateBlock", (float)justTime);
                cooldown = true;
            }
        }
        else if (cooldown)
        {
            cooldown = false;
        }

        /* TWICEブロックの更新 */
        if (block == Block.TWICE && twiceSTime <= _elaspedTime && _elaspedTime <= twiceETime)
        {
            if (!cooldown2)
            {
                justTime = bpm170 * 0.25 - _elaspedTime;
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