using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイマの時刻をもとにTextを更新するコンポーネント
/// </summary>
public class CountDownTextComponent : MonoBehaviour
{

    /// <summary>
    /// UnityEditor上で渡しておく
    /// </summary>
    [SerializeField]
    private CountDownTimer countDownTimer;

    /// <summary>
    /// uGUIのText
    /// </summary>
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();

        //タイマの残り時間を描画する
        countDownTimer
            .CountDownObservable
            .Subscribe(time =>
            {
                //OnNextで時刻の描画
                text.text = string.Format($"{time}");
            }, () =>
            {
                //OnCompleteで文字を消す
                text.text = string.Empty;
            });

        //タイマが1秒以下になったタイミングで色を赤くする
        countDownTimer
            .CountDownObservable
            .First(timer => timer <= 1)
            .Subscribe(_ => text.color = Color.red);
    }
}