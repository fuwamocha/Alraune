using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearStatusNotifier : MonoBehaviour
{
    [SerializeField] private AudioClip _enterPortalAudioClip;
    [SerializeField] private PlayerManager _playerManager;
    public bool isGameClear { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;

    private AudioSource _enterPortalAudioSource;

    private void Start()
    {
        _enterPortalAudioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_playerManager.Hp.Value <= 0)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameClear();
            return;
        }
    }

    private void GameClear()
    {
        // ポータルSEを流す 
        _enterPortalAudioSource.PlayOneShot(_enterPortalAudioClip);

        isGameClear = true;
        SceneManager.LoadScene(Config.GameStatus.GameClear.ToString());
    }

    private void GameOver()
    {
        isGameOver = true;
        SceneManager.LoadScene(Config.GameStatus.GameOver.ToString());
    }
}
