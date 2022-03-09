using UnityEngine;
using UnityEngine.UI;

public class CurrentComboView : MonoBehaviour
{
    [SerializeField] private ComboCounter _combo;
    private Text _currentComboText;

    private void Start()
    {
        _currentComboText = this.GetComponent<Text>();
    }

    private void Update()
    {
        _currentComboText.text = $"{_combo.currentComboCount}";
    }

}
