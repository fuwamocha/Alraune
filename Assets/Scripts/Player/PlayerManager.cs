using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの動作に関するクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private InputReflector _inputReflector;
    [SerializeField] LayerMask groundLayer = default;

    public IReadOnlyReactiveProperty<int> Hp => _hp;
    public bool noJump = false;     // ジャンプのオン/オフ切り替え用

    public RaycastHit2D hit;


    private readonly ReactiveProperty<int> _hp = new ReactiveProperty<int>(12);
    private float xLocal;
    private float xSpeed;
    private float jumpPower = 750f;
    private bool autoJump = false;
    private bool missAnim = false;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 vector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        _hp.AddTo(this);
    }

    private void Update()
    {
        if (_inputReflector.MissHit)
        {
            MissAnimation();
            ReduceHP();
        }
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        /* ショータ君の向き */
        xLocal = Mathf.Sign(transform.localScale.x);
        transform.localScale = new Vector2(xLocal * 0.8f, 0.8f);

        /* 自動ジャンプ処理 */
        if (autoJump && !noJump)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            xSpeed = 6.33f;
            autoJump = false;
        }
        else if (HitGround())
        {
            xSpeed = 0f;
        }

        /* 移動処理 */
        vector.x = xSpeed * xLocal;
        vector.y = rb.velocity.y;
        rb.velocity = vector;
    }

    /// <summary>
    /// プレイヤーのアニメーション処理
    /// </summary>
    private void PlayerAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(xSpeed));

        if (HitGround() && animator.GetBool("Jump"))
        {
            animator.SetBool("Jump", false);
        }
        else if (!HitGround() && !animator.GetBool("Jump"))
        {
            animator.SetBool("Jump", true);
        }

        if (missAnim)
        {

            animator.SetTrigger("Miss");
            missAnim = false;
        }
    }
    public void RhythmAnimation()
    {
        animator.SetTrigger("Rhythm");
    }

    public void MissAnimation()
    {
        missAnim = true;
    }

    public void ReduceHP()
    {
        _hp.Value--;
        _inputReflector.ResetMissFlag();
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