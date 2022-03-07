using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpView : MonoBehaviour
{
    [SerializeField] private Player2Manager _player2Manager;
    private Text _hpText;
    private void Start()
    {

        _hpText = this.GetComponent<Text>();

        // HPが減ったらテキスト更新
        _player2Manager.Hp
            .Where(x => x >= 0)
            .Subscribe(x =>
            {
                UpdateHPText(x);
            });
    }

    private void UpdateHPText(int hp)
    {
        _hpText.text = $"{hp:00}";
        Debug.Log($"残りHP: {hp}");
    }
}
