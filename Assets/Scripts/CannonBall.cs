using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CannonBall : MonoBehaviour
{
    [SerializeField] AudioMixerGroup _audioMixer;
    [SerializeField] AudioClip _audioClipSand;
    [SerializeField] AudioClip _audioClipObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level" || other.tag == "Object")
        {
            GameObject _obj = new GameObject();
            _obj.transform.position = other.gameObject.transform.position;
            AudioSource _sandAudio = _obj.AddComponent<AudioSource>();
            _sandAudio.outputAudioMixerGroup = _audioMixer;
            if (other.tag == "Level")
            {
                _sandAudio.clip = _audioClipSand;
            }
            else
            {
                _sandAudio.clip = _audioClipObject;
            }
            _sandAudio.maxDistance = 100f;
            _sandAudio.volume = 0.7f;
            _obj.AddComponent<CannonBallSound>();
            _sandAudio.Play();
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
