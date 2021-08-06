using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlrauneMovement : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;

    private int count = 0;
    private float xSpeed;
    private float jumpPower = 940f;
    private float x;
    private float y;
    private bool isJump = false;
    private bool canJump = false;

    private Rigidbody2D rb;
    private Vector2 vector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.U)) {
            Warp();
        }
    }

    private void FixedUpdate()
    {
        
        vector.y = rb.velocity.y;
        
        if (canJump && HitGround()) {
            count++;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        } else if (canJump && !HitGround()) {
            canJump = false;
            isJump = true;
        }

        if (isJump && HitGround()) {
            vector.x = 0f;
            isJump = false;
            if (count % 4 == 0) {
                transform.localScale = new Vector2(transform.localScale.x * -1f, 1f);
            }

        } else if (isJump) {
            xSpeed = Mathf.Sign(transform.localScale.x);
            vector.x = xSpeed * 7.6f;
        }

        rb.velocity = vector;
    }

    // 接地判定
    private bool HitGround()
    {
        return Physics2D.Linecast(transform.position - transform.right * 0.18f - transform.up * 1.4f, transform.position - transform.right * 0.18f - transform.up * 1.5f, groundLayer);
    }

    private void Warp()
    {
        count++;

        if (count % 4 == 0) {
            x = -5.3f;
        } else if (count % 4 == 1) {
            x = -3.3f;
        } else if (count % 4 == 2) {
            x = -1.3f;
        } else {
            x = 0.7f;
        }
        if (count % 8 >= 4) {
            x =  -x - 2.7f;
        }

        y = -2.3f + count * 1.2f;

        transform.position = new Vector2(x, y);

        canJump = false;
        isJump = false;

        vector.x = 0f;
        vector.y = 0f;
        rb.velocity = vector;

        if (count % 4 == 0) {
            transform.localScale = new Vector2(transform.localScale.x * -1f, 1f);
        }
    }

    public void CanJump()
    {
        canJump = true;
    }
}
