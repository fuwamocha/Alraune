using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    int StageIndex;

    public GameObject StairStep; //�X�e�[�W�̃v���n�u
    public GameObject StairStepLast;
    // �m�[�c��
    //public int steps = 331 - 16;
    public int line;

    // �W�����v�J�E���g
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
