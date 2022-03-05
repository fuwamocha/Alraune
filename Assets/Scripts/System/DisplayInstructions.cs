using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInstructions : MonoBehaviour
{
    /// <summary>
    /// 説明書画像
    /// </summary>
    [SerializeField] private Image _instructionsImage;

    private void Start()
    {
        _instructionsImage = _instructionsImage.GetComponent<Image>();

        var instButton = this.GetComponent<Button>();
        // ボタンをクリックされたら説明書表示
        instButton.OnClickAsObservable()
                .Subscribe(_ =>
                   ToggleStatus(true)
                )
                .AddTo(this);
    }

    /// <summary>
    /// タイトル画面の説明書ボタンをクリックで表示・説明書画像クリックで非表示
    /// </summary>
    /// <param name="status">説明書の状態</param>
    public void ToggleStatus(bool status)
    {
        _instructionsImage.enabled = status;
    }
}
