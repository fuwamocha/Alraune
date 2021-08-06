using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class TimeCounter : MonoBehaviour
{
   
    // 変数
    public int min = 1;
    public float sec = 57;
    private float totalTime;
    private bool isStart = false;

    //時間表示
    public Text timeText;
 
    
    void Start()
    {
        // 総時間計算
        totalTime = min*60+sec;
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isStart = true;

        // カウントダウン
        totalTime = min*60+sec; 
        if (isStart&&totalTime>=0)
            totalTime -= Time.deltaTime;
 
        // 分秒の更新
        min = (int)totalTime / 60;
        sec = totalTime - min*60;

        // 時間表示
        timeText.text = min.ToString("00") + ":" + ((int)sec).ToString("00");
 
        // カウントダウンが0以下になったとき
        if( totalTime <= 0 )
        {
            // ゲームオーバーの処理
        }
    }
}