using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    [SerializeField] Sprite stick = default;
    [SerializeField] Sprite stickBig = default;

    public static bool isPerfect = false;
    public static bool isSuccess = false;

    private float speed;
    private float time170 = 0.70588235f;
    private float totalTime = 0f;
    private float deathTime;

    private float startTime;
    private float endTime;
    private float successTime;
    private float perfectSTime;
    private float perfectETime;

    private Vector2 vector;
    private Rigidbody2D rb;
    private SpriteRenderer sprRend;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprRend = GetComponent<SpriteRenderer>();

        deathTime = time170 * 2500f;
        speed = 6000f / deathTime;

        startTime = deathTime * 0.65f;
        endTime = deathTime * 0.86f;
        successTime = deathTime * 0.74f;
        perfectSTime = deathTime * 0.78f;
        perfectETime = deathTime * 0.82f;

        sprRend.enabled = false;

        //Debug.Log(successTime);
        //Debug.Log(endTime);
    }

    private void Update()
    {
        totalTime += Time.deltaTime * 1000;

        if (totalTime > endTime) {
            Debug.Log("MISS");
            Destroy(gameObject);
        } else if (totalTime >= successTime) {
            sprRend.sprite = stickBig;
        }

        if (Input.GetKeyDown("space")){
            if (totalTime >= perfectSTime && totalTime <= perfectETime) {
                isPerfect = true;
                Debug.Log("NICE!!");
                
            } else if (totalTime >= successTime) {
                Debug.Log("OK!");
                isSuccess = true;
            } else if (totalTime >= startTime) {
                Debug.Log("MISS");
            } else {
                return;
            }

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        vector.x = -speed;
        rb.velocity = vector;
    }
}
