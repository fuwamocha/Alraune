using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動作に関するクラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;

    public RaycastHit2D hit;

    private float xSpeed;
    //private float jumpPower = 950f;
    private bool autoJump = false;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 vector;


    public bool noJump = false;     // デバッグ用

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (xSpeed != 0)
        {
            transform.localScale = new Vector2(xSpeed * 0.8f, 0.8f);
        }

        /* 移動処理 */
        vector.x = xSpeed * 5f;
        vector.y = rb.velocity.y;
        rb.velocity = vector;

        /* 自動ジャンプ処理 */
        if (autoJump && !noJump)
        {
            transform.Translate(1.265f, 2.0f, 0f);
            autoJump = false;
        }
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

    public void RythemAnim()
    {
        // できれば PlayerAnimation関数 と統合
        animator.SetTrigger("Rythem");
    }

    public void AutoJump()
    {
        autoJump = true;
    }

}
