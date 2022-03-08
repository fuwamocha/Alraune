using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveClearScene : MonoBehaviour
{
    public static bool isClear = false;
    private string _portalTag = "Portal";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_portalTag))
        {
            isClear = true;
            SceneManager.LoadScene(Config.GameStatus.GameClear.ToString());
            return;
        }
    }
}
