using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip Shota = default;
    [SerializeField] AudioClip ShotaEnd = default;
    [SerializeField] AudioClip Title = default;
    [SerializeField] AudioClip BadEnd = default;
    [SerializeField] AudioClip Credit = default;
    [SerializeField] Button FirstSelectButton = default;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        FirstSelectButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShotaStart()
    {
        audio.clip = Shota;
        audio.Play();
    }
    public void ShotaEndStart()
    {
        audio.clip = ShotaEnd;
        audio.Play();
    }
    public void TitleStart()
    {
        audio.clip = Title;
        audio.Play();
    }
    public void BadEndStart()
    {
        audio.clip = BadEnd;
        audio.Play();
    }
    public void CreditStart()
    {
        audio.clip = Credit;
        audio.Play();
    }
}
