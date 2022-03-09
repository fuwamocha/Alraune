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
        bufferTime = Config.StepSecondsPerBeat * 0.70;
        twiceSTime = Config.StepSecondsPerBeat * 0.15;
        twiceETime = Config.StepSecondsPerBeat * 0.25;

        _input.Space
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    _audioSource.PlayOneShot(_inputAudioClip);
                    isPressSpace = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.UpArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    _audioSource.PlayOneShot(_inputAudioClip);
                    isPressUpArrow = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.DownArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    _audioSource.PlayOneShot(_inputAudioClip);
                    isPressDownArrow = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.LeftArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    _audioSource.PlayOneShot(_inputAudioClip);
                    isPressLeftArrow = true;
                    /* pressCooldown = true;
                    _timingJudger.Space(); */
                });

        _input.RightArrow
                .Where(_ => !pressCooldown)
                .Subscribe(x =>
                {
                    _audioSource.PlayOneShot(_inputAudioClip);
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

                Check(0.4);
                break;

            case BlockReader.Block.TWICE:

                if (timerNum == 1)
                {
                    Check(0.25);
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
                    else if (_leashedTime < Config.StepSecondsPerBeat * 0.75 && !pressCooldown)
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
                else if (_leashedTime < Config.StepSecondsPerBeat * 0.4 && !pressCooldown)
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
        else if (_leashedTime < Config.StepSecondsPerBeat * 0.4)
        {
            pressCooldown = true;
            _timingJudger.Space();
        }
    }

    private void Check(double num)
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
        canSpaceTime = Config.StepSecondsPerBeat * 0.72;
        cantSpaceTime = Config.StepSecondsPerBeat * 0.22;      // 押さないブロック用？
        goodStartTime = Config.StepSecondsPerBeat * 0.85;
        goodEndTime = Config.StepSecondsPerBeat * 0.15;
        greatStartTime = Config.StepSecondsPerBeat * 0.92;
        greatEndTime = Config.StepSecondsPerBeat * 0.08;
        exStartTime = Config.StepSecondsPerBeat * 0.97;
        exEndTime = Config.StepSecondsPerBeat * 0.03;
    }

    private void TwiceTimer()
    {
        timerNum = 2;
        canSpaceTime = Config.StepSecondsPerBeat * 0.22;
        cantSpaceTime = Config.StepSecondsPerBeat * 0.72;      // 押さないブロック用？
        goodStartTime = Config.StepSecondsPerBeat * 0.35;
        goodEndTime = Config.StepSecondsPerBeat * 0.65;
        greatStartTime = Config.StepSecondsPerBeat * 0.42;
        greatEndTime = Config.StepSecondsPerBeat * 0.58;
        exStartTime = Config.StepSecondsPerBeat * 0.47;
        exEndTime = Config.StepSecondsPerBeat * 0.53;
    }

    private void CheckTiming()
    {
        /* ブロックの更新 */
        if (_leashedTime >= bufferTime)
        {
            if (!cooldown)
            {
                justTime = Config.StepSecondsPerBeat * 0.75 - _leashedTime;
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
                justTime = Config.StepSecondsPerBeat * 0.25 - _leashedTime;
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
    public void ConvertLocal(double elaspedTime)
    {
        _leashedTime = elaspedTime;

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