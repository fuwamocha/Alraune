using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの動作に関するクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;
    [SerializeField] private AudioClip _inputAudioClip;
    [SerializeField] private AudioSource _audioSource;

    public IReadOnlyReactiveProperty<int> Hp => _hp;
    public bool noJump = false;     // ジャンプのオン/オフ切り替え用

    public bool pressSpace = false;
    public bool upArrow = false;
    public bool downArrow = false;
    public bool leftArrow = false;
    public bool rightArrow = false;
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
        _audioSource = _audioSource.GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        _hp.AddTo(this);
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
    /// プレイヤーのキーボード入力処理
    /// </summary>
    private void PlayerKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressSpace = true;
            _audioSource.PlayOneShot(_inputAudioClip);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upArrow = true;
            _audioSource.PlayOneShot(_inputAudioClip);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downArrow = true;
            _audioSource.PlayOneShot(_inputAudioClip);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftArrow = true;
            _audioSource.PlayOneShot(_inputAudioClip);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightArrow = true;
            _audioSource.PlayOneShot(_inputAudioClip);
        }

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