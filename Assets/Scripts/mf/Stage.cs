using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    int StageIndex;

    public GameObject StairStep; //ステージのプレハブ
    public GameObject StairStepLast;
    // ノーツ数
    //public int steps = 331 - 16;
    public int line;

    // ジャンプカウント
    public int count = 0;
    //Instantiate(StairStep, new Vector3(-4.0f + 8.0f * count, 0.0f + 4.79f * count, 0), Quaternion.identity);

    private void Awake()
    {
        for (count = 0; count < line; count++)
        {
            Instantiate(StairStep, new Vector3(-4.0f + 8.0f * count, 0.0f + 4.79f * count, 0), Quaternion.identity);
        }
    }
}
