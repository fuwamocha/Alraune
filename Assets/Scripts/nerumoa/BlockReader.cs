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
    private bool missTime;
    private bool goodTime;
    private bool greatTime;
    private bool exTime;
    private bool cooldown = true;
    private bool wrongKey = false;
    private RaycastHit2D _hit;

    private string hitBlock;

    private enum Block
    {
        NORMAL,
        TWICE,
        SKIP,
        CROSSUP,
        CROSSDOWN,
        CROSSLEFT,
        CROSSRIGHT
    }

    private Block block;

    public GameObject excellent;
    public GameObject great;
    public GameObject Good;
    public GameObject Miss;

    private void Start()
    {
        excellent.SetActive(false);
        great.SetActive(false);
        Good.SetActive(false);
        Miss.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (bpm170 * 0.70 <= _elaspedTime && _elaspedTime <= bpm170 * 0.75) {
            cooldown = false;   // タイミングを固定化する(Rythem Managerでやってるみたいな感じ) bufferTime
            wrongKey = false;
            ReadBlock();
        }

        PressSpace();

    }


    /// <summary>
    /// ブロックの種類を読み取る
    /// </summary>
    private void ReadBlock()
    {
        if (_hit.collider == null) {
            return;
        }

        hitBlock = _hit.collider.name;
        if (hitBlock == "Normal") {
            block = Block.NORMAL;
        } else if (hitBlock == "Twice") {
            block = Block.TWICE;
        } else if (hitBlock == "Skip") {
            block = Block.SKIP;
        } else if (hitBlock == "CrossUp") {
            block = Block.CROSSUP;
        } else if (hitBlock == "CrossDown") {
            block = Block.CROSSDOWN;
        } else if (hitBlock == "CrossLeft") {
            block = Block.CROSSLEFT;
        } else if (hitBlock == "CrossRight") {
            block = Block.CROSSRIGHT;
        }
    }

    /// <summary>
    /// スペースとリズムの同期処理
    /// </summary>
    private void PressSpace()
    {
        switch (block) {
            case Block.NORMAL:

                AAA();
                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime) {

                    if (player.pressSpace && !cooldown) {
                        cooldown = true;
                        JudgeSpace();
                    }
                } else if (_elaspedTime < bpm170 * 0.4 && !cooldown) {
                    cooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.TWICE:

                AAA();
                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime) {

                    if (player.pressSpace && !cooldown) {
                        cooldown = true;
                        JudgeSpace();
                    }
                } else if (_elaspedTime < bpm170 * 0.4 && !cooldown) {
                    cooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.SKIP:

                AAA();
                if (canSpaceTime <= _elaspedTime || _elaspedTime <= cantSpaceTime) {

                    if (player.pressSpace && !cooldown) {
                        cooldown = true;
                        Skip();
                    }
                } else if (_elaspedTime < bpm170 * 0.4 && !cooldown) {
                    cooldown = true;
                    Skip();
                }
                break;

            case Block.CROSSUP:

                AAA();
                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime) {

                    if (player.upArrow && !cooldown) {
                        cooldown = true;
                        JudgeSpace();
                    } else if (player.downArrow || player.leftArrow || player.rightArrow) {
                        cooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                } else if (_elaspedTime < bpm170 * 0.4 && !cooldown) {
                    cooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.CROSSDOWN:

                AAA();
                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime) {

                    if (player.downArrow && !cooldown) {
                        cooldown = true;
                        JudgeSpace();
                    } else if (player.upArrow || player.leftArrow || player.rightArrow) {
                        cooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                } else if (_elaspedTime < bpm170 * 0.4 && !cooldown) {
                    cooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.CROSSLEFT:

                AAA();
                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime) {

                    if (player.leftArrow && !cooldown) {
                        cooldown = true;
                        JudgeSpace();
                    } else if (player.upArrow || player.downArrow || player.rightArrow) {
                        cooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                } else if (_elaspedTime < bpm170 * 0.4 && !cooldown) {
                    cooldown = true;
                    JudgeSpace();
                }
                break;

            case Block.CROSSRIGHT:

                AAA();
                if (canSpaceTime <= _elaspedTime || _elaspedTime <= goodEndTime) {

                    if (player.rightArrow && !cooldown) {
                        cooldown = true;
                        JudgeSpace();
                    } else if (player.upArrow || player.downArrow || player.leftArrow) {
                        cooldown = true;
                        wrongKey = true;
                        JudgeSpace();
                    }
                } else if (_elaspedTime < bpm170 * 0.4 && !cooldown) {
                    cooldown = true;
                    JudgeSpace();
                }
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
        exTime    = exStartTime    <= _elaspedTime || _elaspedTime <= exEndTime;
        greatTime = greatStartTime <= _elaspedTime || _elaspedTime <= greatEndTime;
        goodTime  = goodStartTime  <= _elaspedTime || _elaspedTime <= goodEndTime;
        missTime  = (goodEndTime < _elaspedTime && _elaspedTime < goodStartTime) || wrongKey;

        if (missTime) {
            Debug.Log("Miss!");
            Miss.SetActive(true);
            StartCoroutine("MissStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(10);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().ComboCount = 0;
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(0);
        } else if (exTime) {
            Debug.Log("Excellent!");
            excellent.SetActive(true);
            StartCoroutine("excellentStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(500);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        } else if (greatTime) {
            Debug.Log("Great!");
            great.SetActive(true);
            StartCoroutine("greatStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(300);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        } else if (goodTime) {
            Debug.Log("Good!");
            Good.SetActive(true);
            StartCoroutine("GoodStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(100);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        } else {
            Debug.Log("ERROR");
        }
    }

    private void Skip()
    {
        missTime = canSpaceTime <= _elaspedTime || _elaspedTime <= cantSpaceTime;

        if (missTime) {
            Debug.Log("Miss!");
            Miss.SetActive(true);
            StartCoroutine("MissStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(10);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().ComboCount = 0;
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(0);
        } else {
            Debug.Log("Excellent!");
            excellent.SetActive(true);
            StartCoroutine("excellentStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(500);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
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

    private void AAA()
    {
        canSpaceTime = bpm170 * 0.75;
        cantSpaceTime = bpm170 * 0.25;      // 押さないブロック用？
        goodStartTime = bpm170 * 0.85;
        goodEndTime = bpm170 * 0.15;
        greatStartTime = bpm170 * 0.92;
        greatEndTime = bpm170 * 0.08;
        exStartTime = bpm170 * 0.97;
        exEndTime = bpm170 * 0.03;
    }

    private void BBB()
    {
        canSpaceTime = bpm170 * 0.25;
        cantSpaceTime = bpm170 * 0.75;      // 押さないブロック用？
        goodStartTime = bpm170 * 0.35;
        goodEndTime = bpm170 * 0.65;
        greatStartTime = bpm170 * 0.42;
        greatEndTime = bpm170 * 0.58;
        exStartTime = bpm170 * 0.47;
        exEndTime = bpm170 * 0.53;
    }

    private IEnumerator excellentStop()
    {
        yield return new WaitForSeconds(0.5f);
        excellent.SetActive(false);
    }
    private IEnumerator greatStop()
    {
        yield return new WaitForSeconds(0.5f);
        great.SetActive(false);
    }
    private IEnumerator GoodStop()
    {
        yield return new WaitForSeconds(0.5f);
        Good.SetActive(false);
    }
    private IEnumerator MissStop()
    {
        yield return new WaitForSeconds(0.5f);
        Miss.SetActive(false);
    }
}
