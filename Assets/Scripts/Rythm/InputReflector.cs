using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class InputReflector : MonoBehaviour
{
    [SerializeField] private TimingJudger _timingJudger;
    [SerializeField] private AudioClip _inputAudioClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] BlockReader _blockReader = default;
    [SerializeField] private KeyInputEventProvider _input;

    private int timerNum;
    private float canSpaceTime;
    private float cantSpaceTime;      // 押さないブロック用？
    private float goodStartTime;
    private float goodEndTime;
    private float greatStartTime;
    private float greatEndTime;
    private float exStartTime;
    private float exEndTime;
    private float _leashedTime = 0f;
    private float bufferTime;
    private float twiceSTime;
    private float twiceETime;
    private float justTime;
    public bool isMissTiming { get; private set; }
    public bool isGoodTiming { get; private set; }
    public bool isGreatTiming { get; private set; }
    public bool isExcellentTiming { get; private set; }
    private bool pressCooldown = true;
    private bool wrongKey = false;
    private bool pressArrows;
    private bool cooldown;
    private bool cooldown2;

    private bool isPressSpace;
    private bool isPressUpArrow;
    private bool isPressDownArrow;
    private bool isPressLeftArrow;
    private bool isPressRightArrow;

    private void Start()
    {
        _audioSource = _audioSource.GetComponent<AudioSource>();
        _input = _input.GetComponent<KeyInputEventProvider>();


        pressArrows = isPressUpArrow || isPressDownArrow || isPressLeftArrow || isPressRightArrow;
        bufferTime = Config.StepSecondsPerBeat * 0.70f;
        twiceSTime = Config.StepSecondsPerBeat * 0.15f;
        twiceETime = Config.StepSecondsPerBeat * 0.25f;

        _input.AnyKey
                .Subscribe(x =>
                {
                    _audioSource.PlayOneShot(_inputAudioClip);
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.Space
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    isPressSpace = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.UpArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    isPressUpArrow = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.DownArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    isPressDownArrow = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.LeftArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    isPressLeftArrow = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.RightArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    isPressRightArrow = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });
    }



    private void FixedUpdate()
    {
        CheckTiming();
        PressSpace();
    }

    /// <summary>
    /// スペースとリズムの同期処理
    /// </summary>
    private void PressSpace()
    {
        switch (_blockReader.block)
        {
            case BlockReader.Block.NORMAL:

                Check(0.4f);
                break;

            case BlockReader.Block.TWICE:

                if (timerNum == 1)
                {
                    Check(0.25f);
                }
                else if (timerNum == 2)
                {
                    if (canSpaceTime <= _leashedTime && _leashedTime <= goodEndTime)
                    {
                        if (isPressSpace && !pressCooldown)
                        {
                            pressCooldown = true;
                            _timingJudger.Space();
                        }
                    }
                    else if (_leashedTime < Config.StepSecondsPerBeat * 0.75f && !pressCooldown)
                    {
                        pressCooldown = true;
                        _timingJudger.Space();
                    }
                }
                break;

            case BlockReader.Block.SKIP:

                if (canSpaceTime <= _leashedTime || _leashedTime <= cantSpaceTime)
                {
                    isMissTiming = true;
                    if ((isPressSpace || pressArrows) && !pressCooldown)
                    {
                        pressCooldown = true;
                        _timingJudger.Skip();
                    }
                }
                else if (_leashedTime < Config.StepSecondsPerBeat * 0.4f && !pressCooldown)
                {
                    isMissTiming = false;
                    pressCooldown = true;

                    _timingJudger.Skip();
                }
                break;

            case BlockReader.Block.CROSSUP:

                CheckCross(isPressUpArrow, isPressDownArrow, isPressLeftArrow, isPressRightArrow);
                break;

            case BlockReader.Block.CROSSDOWN:

                CheckCross(isPressDownArrow, isPressUpArrow, isPressLeftArrow, isPressRightArrow);
                break;

            case BlockReader.Block.CROSSLEFT:

                CheckCross(isPressLeftArrow, isPressUpArrow, isPressDownArrow, isPressRightArrow);
                break;

            case BlockReader.Block.CROSSRIGHT:

                CheckCross(isPressRightArrow, isPressUpArrow, isPressLeftArrow, isPressDownArrow);
                break;

            case BlockReader.Block.NONE:
                break;
        }

        isPressSpace = false;
        isPressUpArrow = false;
        isPressDownArrow = false;
        isPressLeftArrow = false;
        isPressRightArrow = false;
    }

    private void CheckCross(bool pressArrow, bool otherArrow1, bool otherArrow2, bool otherArrow3)
    {
        if (pressCooldown) return;

        if (canSpaceTime <= _leashedTime || _leashedTime <= goodEndTime)
        {
            if (pressArrow)
            {
                pressCooldown = true;
                _timingJudger.Space();
            }
            else if (otherArrow1 || otherArrow2 || otherArrow3)
            {
                pressCooldown = true;
                wrongKey = true;
                _timingJudger.Space();
            }
        }
        else if (_leashedTime < Config.StepSecondsPerBeat * 0.4f)
        {
            pressCooldown = true;
            _timingJudger.Space();
        }
    }

    private void Check(float num)
    {
        if (pressCooldown) return;

        if (canSpaceTime <= _leashedTime || _leashedTime <= goodEndTime)
        {
            if (isPressSpace)
            {
                pressCooldown = true;
                _timingJudger.Space();
            }
        }
        else if (_leashedTime < Config.StepSecondsPerBeat * num)
        {
            pressCooldown = true;
            _timingJudger.Space();
        }
    }


    public void JudgeTime()
    {
        if (timerNum == 1)
        {
            isExcellentTiming = exStartTime <= _leashedTime || _leashedTime <= exEndTime;
            isGreatTiming = greatStartTime <= _leashedTime || _leashedTime <= greatEndTime;
            isGoodTiming = goodStartTime <= _leashedTime || _leashedTime <= goodEndTime;
            isMissTiming = (goodEndTime < _leashedTime && _leashedTime < goodStartTime) || wrongKey;
        }
        else if (timerNum == 2)
        {
            isExcellentTiming = exStartTime <= _leashedTime && _leashedTime <= exEndTime;
            isGreatTiming = greatStartTime <= _leashedTime && _leashedTime <= greatEndTime;
            isGoodTiming = goodStartTime <= _leashedTime && _leashedTime <= goodEndTime;
            isMissTiming = goodEndTime < _leashedTime || _leashedTime < goodStartTime || wrongKey;
        }
    }


    private void NormalTimer()
    {
        timerNum = 1;
        canSpaceTime = Config.StepSecondsPerBeat * 0.72f;
        cantSpaceTime = Config.StepSecondsPerBeat * 0.22f;      // 押さないブロック用？
        goodStartTime = Config.StepSecondsPerBeat * 0.85f;
        goodEndTime = Config.StepSecondsPerBeat * 0.15f;
        greatStartTime = Config.StepSecondsPerBeat * 0.92f;
        greatEndTime = Config.StepSecondsPerBeat * 0.08f;
        exStartTime = Config.StepSecondsPerBeat * 0.97f;
        exEndTime = Config.StepSecondsPerBeat * 0.03f;
    }

    private void TwiceTimer()
    {
        timerNum = 2;
        canSpaceTime = Config.StepSecondsPerBeat * 0.22f;
        cantSpaceTime = Config.StepSecondsPerBeat * 0.72f;      // 押さないブロック用？
        goodStartTime = Config.StepSecondsPerBeat * 0.35f;
        goodEndTime = Config.StepSecondsPerBeat * 0.65f;
        greatStartTime = Config.StepSecondsPerBeat * 0.42f;
        greatEndTime = Config.StepSecondsPerBeat * 0.58f;
        exStartTime = Config.StepSecondsPerBeat * 0.47f;
        exEndTime = Config.StepSecondsPerBeat * 0.53f;
    }

    private void CheckTiming()
    {
        /* ブロックの更新 */
        if (_leashedTime >= bufferTime)
        {
            if (!cooldown)
            {
                justTime = Config.StepSecondsPerBeat * 0.75f - _leashedTime;
                Invoke("UpdateBlock", (float)justTime);
                cooldown = true;
            }
        }
        else if (cooldown)
        {
            cooldown = false;
        }

        /* TWICEブロックの更新 */
        if (_blockReader.block == BlockReader.Block.TWICE && twiceSTime <= _leashedTime && _leashedTime <= twiceETime)
        {
            if (!cooldown2)
            {
                justTime = Config.StepSecondsPerBeat * 0.25f - _leashedTime;
                Invoke("UpdateTwice", (float)justTime);
                cooldown2 = true;
            }
        }
        else if (cooldown2)
        {
            cooldown2 = false;
        }
    }


    /// <summary>
    /// BlockReaderクラスで扱えるローカル変数に変換
    /// </summary>
    public void ConvertLocal(float elapsedTime)
    {
        _leashedTime = elapsedTime;

    }
    private void UpdateBlock()
    {
        NormalTimer();
        pressCooldown = false;
        wrongKey = false;
        _blockReader.ReadBlock();
    }

    private void UpdateTwice()
    {
        TwiceTimer();
        pressCooldown = false;
        wrongKey = false;
    }
}
