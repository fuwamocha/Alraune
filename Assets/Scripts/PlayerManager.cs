using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;
    [SerializeField] AudioClip shan = default;

    private int count = 0;
    private float xSpeed;
    private float xScale;
    private float jumpPower = 950f;
    private bool isJump = false;
    private bool canJump = false;
    private bool autoJump = false;

    private Animator animator;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Vector2 vector;

    private void Start()
    {
        rb       = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource   = GetComponent<AudioSource>();
        audioSource.clip = shan;
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

        /*
        // デバッグ用　のけぞり
        if (Input.GetKeyDown(KeyCode.U)) {
            animator.SetTrigger("Miss");
        }
        */
    }

    private void FixedUpdate()
    {
        // プレイヤーの向き
        if (xSpeed != 0) {
            transform.localScale = new Vector2(xSpeed * 1.25f, 1.25f);
        }

        // 移動処理
        vector.x = xSpeed * 5f;
        vector.y = rb.velocity.y;

        // オートジャンプ処理
        if (autoJump) {
            if (canJump && HitGround()) {
                count++;
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            } else if (canJump && !HitGround()) {
                canJump = false;
                isJump = true;
            }

            if (isJump && HitGround()) {
                xSpeed = 0f;
                isJump = false;
                autoJump = false;
                if (count % 4 == 0) {
                    transform.localScale = new Vector2(transform.localScale.x * -1f, 1.25f);
                }
            } else if (isJump) {
                xSpeed = Mathf.Sign(transform.localScale.x);
                vector.x = xSpeed * 7.6f;
            }
        }

        // 移動処理
        rb.velocity = vector;
    }

    // 接地判定
    private bool HitGround()
    {
        return Physics2D.Linecast(transform.position - (transform.right * 0.18f * xScale) - transform.up * 1.85f, transform.position - (transform.right * 0.18f * xScale) - transform.up * 1.95f, groundLayer);
    }

    // アニメーション遷移
    public void RythemAnim()
    {
        animator.SetTrigger("Rythem");
    }

    public void AutoJump()
    {
        canJump = true;
        autoJump = true;
    }

    public void Shan()
    {
        audioSource.Play();
    }
}