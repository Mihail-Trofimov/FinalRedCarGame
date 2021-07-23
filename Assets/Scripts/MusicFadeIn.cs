using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeIn : MonoBehaviour
{
    public AudioSource _music;
    void Update()
    {
        if (_music != null)
        {
            if (!_music.isPlaying)
            {
                _music.Play();
            }
            if (_music.volume < 1f)
            {
                _music.volume += 0.2f;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
