using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Manager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;

    private float xSpeed;
    //private float jumpPower = 950f;
    private bool autoJump = false;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 vector;

    public bool noJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal");

        /* アニメーション処理 */ 
        animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        if (HitGround() && animator.GetBool("Jump")) {
            animator.SetBool("Jump", false);
        } else if (!HitGround() && !animator.GetBool("Jump")) {
            animator.SetBool("Jump", true);
        }
        
    }

    private void FixedUpdate()
    {
        if (xSpeed != 0) {
            transform.localScale = new Vector2(xSpeed * 0.8f, 0.8f);
        }

        /* 自動ジャンプ処理 */
        if (autoJump && !noJump) {
            transform.Translate(1.265f, 2.0f, 0f);
            autoJump = false;
        }

        /* 移動処理 */
        vector.x = xSpeed * 5f;
        vector.y = rb.velocity.y;
        rb.velocity = vector;
    }

    /* 接地判定の取得 */
    private bool HitGround()
    {
        // Debug.DrawLine(transform.position - transform.up * 1.15f, transform.position - transform.up * 1.30f, Color.red);
        return Physics2D.Linecast(transform.position - transform.up * 1.15f, transform.position - transform.up * 1.30f, groundLayer);
    }

    public void RythemAnim()
    {
        animator.SetTrigger("Rythem");
    }

    public void AutoJump()
    {
        autoJump = true;
    }
}
