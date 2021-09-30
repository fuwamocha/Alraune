using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̓���Ɋւ���N���X
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


    public bool noJump = false;     // �f�o�b�O�p

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

        /* �ړ����� */
        vector.x = xSpeed * 5f;
        vector.y = rb.velocity.y;
        rb.velocity = vector;

        /* �����W�����v���� */
        if (autoJump && !noJump)
        {
            transform.Translate(1.265f, 2.0f, 0f);
            autoJump = false;
        }
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

    public void RythemAnim()
    {
        // �ł���� PlayerAnimation�֐� �Ɠ���
        animator.SetTrigger("Rythem");
    }

    public void AutoJump()
    {
        autoJump = true;
    }

}
