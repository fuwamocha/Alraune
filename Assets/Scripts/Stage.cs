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
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {


            while (count < line)
            {
                count++;
                Instantiate(StairStep, new Vector3(-0.35f + 8.0f * count, -2.59f + 4.79f * count, 0), Quaternion.identity);
            }
            if (count == line)
            {
                count++;
                Instantiate(StairStepLast, new Vector3(-0.35f + 8.0f * count, -2.59f + 4.79f * count, 0), Quaternion.identity);
            }
        }
    }
}
