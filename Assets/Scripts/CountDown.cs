using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Text _textCountdown;

    // Start is called before the first frame update
    void Start()
    {
        _textCountdown.text = "";
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        //_imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "3";
        yield return new WaitForSeconds(1);

        _textCountdown.text = "2";
        yield return new WaitForSeconds(1);

        _textCountdown.text = "1";
        yield return new WaitForSeconds(1);

        _textCountdown.text = "Start!";
        yield return new WaitForSeconds(1);

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
}
