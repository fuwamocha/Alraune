using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timing2Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StickManager.isPerfect) {
            //Debug.Log("aaa");
        }



        if (StickManager.isSuccess) {
                //Debug.Log("bbb");
        }
    }
}
