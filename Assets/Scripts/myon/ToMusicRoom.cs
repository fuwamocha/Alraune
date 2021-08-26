using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMusicRoom : MonoBehaviour
{

    public void GotoMusicRoom()
    {
        SceneManager.LoadScene("MusicRoom");
    }
}
