using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingJudger : MonoBehaviour
{

    [SerializeField] private NotesJudgementPopper _notesJudgementPopper;
    [SerializeField] private InputReflector _inputReflector;
    [SerializeField] private GameObject _excellentObj;
    [SerializeField] private GameObject _greatObj;
    [SerializeField] private GameObject _goodObj;
    [SerializeField] private GameObject _missObj;


    public bool MissHit { get; private set; }

    /// <summary>
    /// スペースのタイミング判定処理
    /// </summary>

    public void Space()
    {
        _inputReflector.JudgeTime();
        if (_inputReflector.isMissTiming)
        {
            MissHit = true;
            _notesJudgementPopper.Failure(_missObj, 0);
        }
        else if (_inputReflector.isExcellentTiming)
        {
            _notesJudgementPopper.Success(_excellentObj, 500);
        }
        else if (_inputReflector.isGreatTiming)
        {
            _notesJudgementPopper.Success(_greatObj, 300);
        }
        else if (_inputReflector.isGoodTiming)
        {
            _notesJudgementPopper.Success(_goodObj, 100);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    public void Skip()
    {
        if (_inputReflector.isMissTiming)
        {
            MissHit = true;
            _notesJudgementPopper.Failure(_missObj, 0);
        }
        else
        {
            _notesJudgementPopper.Success(_excellentObj, 500);
        }
        //audio.Play();
    }

    public void ResetMissFlag()
    {
        MissHit = false;
    }
}
