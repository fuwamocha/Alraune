using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �v���C���[�̓���Ɋւ���N���X
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;

    public bool noJump = false;     // �W�����v�̃I��/�I�t�؂�ւ��p

    private int count = 0;
    private int missCount = 0;
    private float x;
    private float y;
    private float xLocal;
    private float xSpeed;
  �@private float jumpPower = 800f;
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
        /* �A���E���l�̌��� */
        xLocal = Mathf.Sign(transform.localScale.x);
        transform.localScale = new Vector2(xLocal * 0.6f, 0.6f);

        /* �����W�����v���� */
        if (autoJump && !noJump) {
            count++;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            xSpeed = 5.755f;
            autoJump = false;
        } else if (HitGround()) {
            xSpeed = 0f;
        }

        /* ���[�v���� */
        if (BlockReader.isMiss) {
            Warp();
        }

        /* �ړ����� */
        vector.x = xSpeed * xLocal;
        vector.y = rb.velocity.y;
        rb.velocity = vector;
    }

    /// <summary>
    /// �ڒn����̎擾
    /// </summary>
    private bool HitGround()
    {
        Debug.DrawLine(transform.position - transform.up * 1.15f,   // �f�o�b�O�p
                    �@ transform.position - transform.up * 1.30f,
                       Color.red);

        hit = Physics2D.Linecast(transform.position - transform.up * 1.15f,
                                 transform.position - transform.up * 1.30f,
                                 groundLayer);
        return hit;
    }

    public void Warp()
    {
        missCount++;
        BlockReader.isMiss = false;

        if (missCount % 2 == 0) {
            xSpeed = 0f;
            x = -13.9f + 1.266f * (count + missCount/2);
            y = -7.6f + 0.76f * (count + missCount/2);
            transform.position = new Vector2(x, y);
        }

        if (missCount == 8) {
            SceneManager.LoadScene("gameover");
        }
    }

    public void AutoJump()
    {
        autoJump = true;
    }
}