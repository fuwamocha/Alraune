using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearMove : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public static bool isClear = false;
    private string  _playerTag = "Player";

    void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == _playerTag) {
            isClear = true;
			SceneManager.LoadScene (_sceneName);
		}
	}
}
