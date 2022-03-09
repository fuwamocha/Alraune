using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpView : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    private Text _hpText;
    private void Start()
    {

        _hpText = this.GetComponent<Text>();

        // HPが減ったらテキスト更新
        _playerManager.Hp
            .Where(x => x >= 0)
            .Subscribe(x =>
            {
                DisplayHPText(x);
            });
    }

    private void DisplayHPText(int hp)
    {
        _hpText.text = $"{hp:00}";
        Debug.Log($"残りHP: {hp}");
    }
}
