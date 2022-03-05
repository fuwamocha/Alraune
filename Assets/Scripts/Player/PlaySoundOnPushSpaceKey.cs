using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnPushSpaceKey : MonoBehaviour
{
    [SerializeField] private AudioClip _spaceAudioClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.PlayOneShot(_spaceAudioClip);
        }
    }
}
