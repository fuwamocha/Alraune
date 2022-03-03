using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMove : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private string _playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == _playerTag)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
