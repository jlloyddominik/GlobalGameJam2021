using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html
public class MusicClass : MonoBehaviour
{
    public static int Score;
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}