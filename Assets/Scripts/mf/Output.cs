using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    public GameObject OkText;
    public GameObject GoodText;
    public GameObject MissText;

    // Start is called before the first frame update
    void Start()
    {
        OkText.SetActive(false);
        GoodText.SetActive(false);
        MissText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OkText.SetActive(true);
            StartCoroutine("OkStop");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GoodText.SetActive(true);
            StartCoroutine("GoodStop");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MissText.SetActive(true);
            StartCoroutine("MissStop");
        }
    }

    private IEnumerator OkStop()
    {
        yield return new WaitForSeconds(1.0f);
        OkText.SetActive(false);
    }
    private IEnumerator GoodStop()
    {
        yield return new WaitForSeconds(1.0f);
        GoodText.SetActive(false);
    }
    private IEnumerator MissStop()
    {
        yield return new WaitForSeconds(1.0f);
        MissText.SetActive(false);
    }
}
