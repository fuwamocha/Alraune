using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnterPortalSound : MonoBehaviour
{
    [SerializeField] private AudioClip _enterPortalAudioClip;
    private AudioSource _enterPortalAudioSource;

    private void Start()
    {
        _enterPortalAudioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (MoveClearScene.isClear) PlaySound();
    }

    private void PlaySound()
    {
        _enterPortalAudioSource.PlayOneShot(_enterPortalAudioClip);
    }
}
