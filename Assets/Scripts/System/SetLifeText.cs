using UnityEngine;
using UnityEngine.UI;

public class SetLifeText : MonoBehaviour
{
    [SerializeField] private Player2Manager _player2Manager;
    [SerializeField] private EnemyManager _enemyManager;
    private Text _lifeText;
    private int _hp;
    private int _missCount;
    private void Start()
    {
        _player2Manager = _player2Manager.GetComponent<Player2Manager>();
        _enemyManager = _enemyManager.GetComponent<EnemyManager>();
        _lifeText = this.GetComponent<Text>();

        UpdateHPText();
    }

    private void Update()
    {
        if (_missCount == _enemyManager.missCount) return;

        UpdateHPText();
    }

    private void UpdateHPText()
    {
        /*ローカルカウント更新*/
        _missCount = _enemyManager.missCount;

        /*残りHP更新*/
        _hp = _player2Manager.hp - _enemyManager.missCount;

        _lifeText.text = $"{_hp:00}";
        Debug.Log($"残りHP: {_hp}");
    }
}
