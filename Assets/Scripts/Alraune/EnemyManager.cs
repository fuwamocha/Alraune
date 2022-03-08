using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤーの動作に関するクラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private InputReflector _inputReflector;
    [SerializeField] LayerMask groundLayer = default;
    [SerializeField] PlayerManager _player2Manager;

    public int missCount { get; private set; } = 0;
    public bool noJump = false;     // ジャンプのオン/オフ切り替え用
    public static bool isNotClear = false;

    private int count = 0;
    private float x;
    private float y;
    private float xLocal;
    private float xSpeed;
    private float jumpPower = 800f;
    private bool autoJump = false;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 vector;
    private RaycastHit2D hit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        /* アルラウネの向き */
        xLocal = Mathf.Sign(transform.localScale.x);
        transform.localScale = new Vector2(xLocal * 0.6f, 0.6f);

        /* 自動ジャンプ処理 */
        if (autoJump && !noJump)
        {
            count++;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            xSpeed = 5.755f;
            autoJump = false;
        }
        else if (HitGround())
        {
            xSpeed = 0f;
        }

        /* ワープ処理 */
        if (_inputReflector.MissHit)
        {
            Warp();
        }

        /* 移動処理 */
        vector.x = xSpeed * xLocal;
        vector.y = rb.velocity.y;
        rb.velocity = vector;
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

    public void Warp()
    {
        missCount++;


        if (missCount % 3 == 0)
        {
            xSpeed = 0f;
            x = -13.9f + 1.266f * (count + missCount / 3);
            y = -7.6f + 0.76f * (count + missCount / 3);
            transform.position = new Vector2(x, y);
        }

        if (_player2Manager.Hp.Value <= 0)
        {
            isNotClear = true;
            SceneManager.LoadScene(Config.GameStatus.GameOver.ToString());
        }
    }

    public void AutoJump()
    {
        autoJump = true;
    }
}