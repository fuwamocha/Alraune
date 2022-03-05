using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class PopParticle : MonoBehaviour
{
    private void Start()
    {
        var spriteRenderer = this.GetComponent<SpriteRenderer>();

        transform.DOLocalJump(
                new Vector3(-5, 0, 0), // 移動終了地点
                1.5f,                    // ジャンプの高さ
                1,                     // ジャンプの総数
                1.5f                   // 演出時間
            )
            .SetRelative()
            .SetLink(this.gameObject);

        DOTween.ToAlpha(
                () => spriteRenderer.color,
                color => spriteRenderer.color = color,
                0f,                                // 最終的なalpha値
                1f
            )
            .SetLink(this.gameObject);

        Destroy(this.gameObject, 0.5f);
    }
}
