using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class close : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("title");
    }
}
