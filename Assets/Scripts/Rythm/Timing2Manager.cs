using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timing2Manager : MonoBehaviour
{
    [SerializeField] AlrauneMovement alrauneMovement = default;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StickManager.isMiss) {
            count++;
            if (count % 4 == 0) { alrauneMovement.Warp(); }
            StickManager.isMiss = false;
        }
    }
}
