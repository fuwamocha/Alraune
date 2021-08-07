using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timing2Manager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StickManager.isPerfect) {
            playerManager.Shan();
        }



        if (StickManager.isSuccess) {
            //Debug.Log("bbb");
        }
    }
}
