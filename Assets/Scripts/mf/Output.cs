using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    public GameObject Ok;
    public GameObject Good;
    public GameObject Miss;

    // Start is called before the first frame update
    void Start()
    {
        Ok.SetActive(false);
        Good.SetActive(false);
        Miss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Ok.SetActive(true);
            StartCoroutine("OkStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(500);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Good.SetActive(true);
            StartCoroutine("GoodStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(100);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Miss.SetActive(true);
            StartCoroutine("MissStop");

            GameObject.Find("ScoreText").GetComponent<Score>().AddScore(10);
            GameObject.Find("ComboText").GetComponent<ComboSystem>().ComboCount = 0;
            GameObject.Find("ComboText").GetComponent<ComboSystem>().AddCombo(0);
        }
    }

    private IEnumerator OkStop()
    {
        yield return new WaitForSeconds(0.5f);
        Ok.SetActive(false);
    }
    private IEnumerator GoodStop()
    {
        yield return new WaitForSeconds(0.5f);
        Good.SetActive(false);
    }
    private IEnumerator MissStop()
    {
        yield return new WaitForSeconds(0.5f);
        Miss.SetActive(false);
    }
}
