using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopSystem : MonoBehaviour
{
    [SerializeField] private GameObject a;

    //スタートと終わりの目印
    public Transform startMarker;
    public Transform endMarker;

    // スピード
    public float speed = 1.0F;

    //二点間の距離を入れる
    private float distance_two;

    void Start()
    {
        //二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(startMarker.position, endMarker.position);
        //a.SetActive(false);
        Invoke("Stop", 2);
    }

    void Update()
    {

        // 現在の位置
        float present_Location = (Time.time * speed) / distance_two;

        // オブジェクトの移動(ここだけ変わった！)
        transform.position = Vector3.Slerp(startMarker.position, endMarker.position, present_Location);
    }

    void Stop()
    {
        a.SetActive(false);
    }
}
