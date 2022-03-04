using UnityEngine;
using UnityEngine.UI;

public class SelectMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _titleMusic = default;
    [SerializeField] private AudioClip _gameMusic = default;
    [SerializeField] private AudioClip _gameClearMusic = default;
    [SerializeField] private AudioClip _gameOverMusic = default;
    [SerializeField] private AudioClip _creditMusic = default;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void StartMusicInTitle()
    {
        _audioSource.clip = _titleMusic;
        _audioSource.Play();
    }
    public void StartMusicInGame()
    {
        _audioSource.clip = _gameMusic;
        _audioSource.Play();
    }
    public void StartMusicInGameClear()
    {
        _audioSource.clip = _gameClearMusic;
        _audioSource.Play();
    }
    public void StartMusicInGameOver()
    {
        _audioSource.clip = _gameOverMusic;
        _audioSource.Play();
    }
    public void StartMusicInCredit()
    {
        _audioSource.clip = _creditMusic;
        _audioSource.Play();
    }
}
