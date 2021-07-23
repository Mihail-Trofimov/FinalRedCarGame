using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeOut : MonoBehaviour
{
    public AudioSource _music;

    void Update()
    {
        if (_music != null)
        {
            if (_music.volume > 0f)
            {
                _music.volume -= 0.2f;
            }
            else
            {
                _music.Pause();
                Destroy(this);
            }
        }
    }
}
