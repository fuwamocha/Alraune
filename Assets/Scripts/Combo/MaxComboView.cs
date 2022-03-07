using UnityEngine;
using UnityEngine.UI;

public class MaxComboView : MonoBehaviour
{
    private Text _maxComboText;

    void Start()
    {
        _maxComboText = this.GetComponent<Text>();

        var maxCombo = PlayerPrefs.GetInt("COMBO");
        _maxComboText.text = $"{maxCombo}";
    }

}
