using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カウントダウンするコンポーネント
/// </summary>
public class CountDownTimer : MonoBehaviour
{
    /// <summary>
    /// カウントダウンストリーム
    /// このObservableを各クラスがSubscribeする
    /// </summary>
    public IObservable<int> CountDownObservable
    {
        get
        {
            return _countDownObservable.AsObservable();
        }
    }

    private IConnectableObservable<int> _countDownObservable;

    void Awake()
    {
        //3秒カウントのストリームを作成
        //PublishでHot変換
        _countDownObservable = CreateCountDownObservable(4).Publish();
    }

    void Start()
    {
        //Start時にカウント開始
        _countDownObservable.Connect();
    }

    /// <summary>
    /// CountTimeだけカウントダウンするストリーム
    /// </summary>
    /// <param name="CountTime"></param>
    /// <returns></returns>
    private IObservable<int> CreateCountDownObservable(int CountTime)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(Config.StepSecondsPerBeat))
            .Select(x => (int)(CountTime - x))
            .TakeWhile(x => x > 0);
    }
}