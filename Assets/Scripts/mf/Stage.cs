using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    //int StageIndex;

    public GameObject StairStep; //�X�N���v�g�̖���prefab
    public GameObject StairStepLast; //�X�N���v�g�̖���prefab
    public int line;
    public int count = 0;

    private void Start()
    {
    }

    private void Update()
    {
        while (count < line)
        {
            count++;
            Instantiate(StairStep, new Vector3(-4.0f + 8.0f * count, 0.0f + 4.79f * count, 0), Quaternion.identity);
        }
        if (count == line)
        {
            count++;
            Instantiate(StairStepLast, new Vector3(-4.0f + 8.0f * count, 0.0f + 4.79f * count, 0), Quaternion.identity);
        }
    }
}
