using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    [SerializeField] GameObject stick = default;                // 生成・移動させるオブジェクト
    [SerializeField] PlayerManager playerManager = default;
    [SerializeField] AlrauneMovement alrauneMovement = default;

    private float totalTime = 0f;
    private float timing = 0f;
    private float time170 = 0.70588235f;
    private bool isRythem = false;
    private bool autoJump = false;

    private bool isStick = false;

    private void Update()
    {
        totalTime += Time.deltaTime * 1000f;        // トータル経過時間(ms)
        timing = totalTime % (time170 * 1000f);     // 1回毎の経過時間(ms)

        if (Input.GetKeyDown("space") && !autoJump) {
            autoJump = true;
        }

        if (timing >= time170 * 1000f - 565f && timing < time170 * 1000f - 535f) {  // 140f～170f の誤差？

            if (autoJump) {
                playerManager.AutoJump();
            }




            if (!isStick) {
                Instantiate(stick, transform);
                isStick = true;
            }
        } else if (isStick) {
            isStick = false;
        }

        if (timing >= time170 * 1000f - 250f && timing < time170 * 1000f - 220f && !isRythem) {
            playerManager.RythemAnim();
            isRythem = true;
        } else if (timing >= time170 * 1000f - 190f && isRythem) {
            isRythem = false;
        }
    }

    
}
