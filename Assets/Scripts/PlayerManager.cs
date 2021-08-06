using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;

    private float xSpeed;
    private float xScale;
    private float jumpPower = 950f;
    private float waitTime;
    private float time2;
    private float time3;
    private bool isJump = false;
    private bool canJump = false;
    private bool autoJump = false;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 vector;


    private void Start()
    {
        rb       = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        waitTime = 0.70588235294f;
        time2 = 0.35f;
        time3 = waitTime - time2;
    }

    private void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal");
        xScale = Mathf.Sign(transform.localScale.x);

        //Debug.DrawLine(transform.position - (transform.right * 0.18f * xScale) - transform.up * 1.85f, transform.position - (transform.right * 0.18f * xScale) - transform.up * 1.95f, Color.red);

        // アニメーション遷移
        animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        if (HitGround() && animator.GetBool("Jump")) {
            animator.SetBool("Jump", false);
        } else if (!HitGround() && !animator.GetBool("Jump")) {
            animator.SetBool("Jump", true);
        }

        // ジャンプ判定
        if (Input.GetKeyDown("space") && !autoJump) {
            StartCoroutine("AutoJumpStart");
            autoJump = true;
        }

        // デバッグ用　のけぞり
        if (Input.GetKeyDown(KeyCode.U)) {
            animator.SetTrigger("Miss");
        }


    }

    private void FixedUpdate()
    {
        // プレイヤーの向き
        if (xSpeed != 0) {
            transform.localScale = new Vector2(xSpeed * 1.25f, 1.25f);
        }

        /*
        // ジャンプ処理
        if (pressJump && HitGround()) {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            pressJump = false;
        }
        */

        // 移動処理
        vector.x = xSpeed * 5f;
        vector.y = rb.velocity.y;

        // オートジャンプ処理
        if (autoJump) {
            if (canJump && HitGround()) {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            } else if (canJump && !HitGround()) {
                canJump = false;
                isJump = true;
            }

            if (isJump && HitGround()) {
                xSpeed = 0f;
                isJump = false;
            } else if (isJump) {
                xSpeed = Mathf.Sign(transform.localScale.x);
                vector.x = xSpeed * 7.6f;
            }
        }

        // 移動処理
        rb.velocity = vector;
    }

    private bool HitGround()
    {
        return Physics2D.Linecast(transform.position - (transform.right * 0.18f * xScale) - transform.up * 1.85f, transform.position - (transform.right * 0.18f * xScale) - transform.up * 1.95f, groundLayer);
    }

    public void RythemAnim()
    {
        animator.SetTrigger("Rythem");
    }

    IEnumerator AutoJumpStart()
    {
        for (int i = 0; i < 3; i++) {
            canJump = true;
            if (i == 2) {
                yield return new WaitForSeconds(time2);
                transform.localScale = new Vector2(transform.localScale.x * -1f, 1.25f);
                yield return new WaitForSeconds(time3);
                break;
            }
            yield return new WaitForSeconds(waitTime);
        }

        StartCoroutine("AutoJump");
    }

    IEnumerator AutoJump()
    {
        for (int i = 0; i < 4; i++) {
            canJump = true;
            if (i == 3) {
                yield return new WaitForSeconds(time2);
                transform.localScale = new Vector2(transform.localScale.x * -1f, 1.25f);
                yield return new WaitForSeconds(time3);
                break;
            }
            yield return new WaitForSeconds(waitTime);
        }

        StartCoroutine("AutoJump");
    }
}
