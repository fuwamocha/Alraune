using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToRanking : MonoBehaviour
{
public void GotoRanking()
    {
        SceneManager.LoadScene("naichilab/unity-simple-ranking/Scenes/Ranking");
    }
}
