using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動作に関するクラス
/// </summary>
public class Player2Manager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;

    public bool noJump = false;     // ジャンプのオン/オフ切り替え用

    public bool pressSpace = false;
    public bool upArrow = false;
    public bool downArrow = false;
    public bool leftArrow = false;
    public bool rightArrow = false;
    public RaycastHit2D hit;

    private float xLocal;
    private float xSpeed;
  　private float jumpPower = 750f;
    private bool autoJump = false;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 vector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerKeyboard();
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        /* ショータ君の向き */
        xLocal = Mathf.Sign(transform.localScale.x);
        transform.localScale = new Vector2(xLocal * 0.8f, 0.8f);

        /* 自動ジャンプ処理 */
        if (autoJump && !noJump) {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            xSpeed = 6.32515f;
            autoJump = false;

            //transform.Translate(1.265f, 2.0f, 0f);
        } else if (HitGround()) {
            xSpeed = 0f;
        }

        /* 移動処理 */
        vector.x = xSpeed * xLocal;
        vector.y = rb.velocity.y;
        rb.velocity = vector;
    }


    /// <summary>
    /// プレイヤーのキーボード入力処理
    /// </summary>
    private void PlayerKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            pressSpace = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            upArrow = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            downArrow = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            leftArrow = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rightArrow = true;
        }
    }

    /// <summary>
    /// プレイヤーのアニメーション処理
    /// </summary>
    private void PlayerAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        if (HitGround() && animator.GetBool("Jump")) {
            animator.SetBool("Jump", false);
        } else if (!HitGround() && !animator.GetBool("Jump")) {
            animator.SetBool("Jump", true);
        }
    }
    public void RythemAnim()
    {
        animator.SetTrigger("Rythem");
    }

    /// <summary>
    /// 接地判定の取得
    /// </summary>
    private bool HitGround()
    {
        Debug.DrawLine(transform.position - transform.up * 1.15f,   // デバッグ用
                    　 transform.position - transform.up * 1.30f,
                       Color.red);

        hit = Physics2D.Linecast(transform.position - transform.up * 1.15f,
                                 transform.position - transform.up * 1.30f,
                                 groundLayer);
        return hit;
    }

    public void AutoJump()
    {
        autoJump = true;
    }
}
