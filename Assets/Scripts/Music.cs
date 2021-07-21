using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private GameMenu _menuScript;

    [SerializeField] private AudioSource _sandPart1;
    [SerializeField] private AudioSource _sandPart2;
    [SerializeField] private AudioSource _sandPart3;
    [SerializeField] private AudioSource _sandEnemy;

    [SerializeField] private GameObject _mound1;
    [SerializeField] private GameObject _mound2;
    [SerializeField] private GameObject _mound3;

    [SerializeField] private BlueCar _enemy;

    [SerializeField] private AudioSource _zonePart1;
    [SerializeField] private AudioSource _zonePart2;
    [SerializeField] private AudioSource _zonePart3;

    [SerializeField] private Zone1End _zone1Script;
    [SerializeField] private Zone2End _zone2Script;
    [SerializeField] private ArcadeZone _zone3Script;

    [SerializeField] private GameObject _zoneObj;

    [SerializeField] private AudioSource _menuMusic;

    private AudioSource _music;

    void Start()
    {
        if(_menuScript._menu) StartCoroutine(MenuMusic());
        else if (_zoneObj.activeSelf && !_zone1Script._end) StartCoroutine(Zone1LoopIE());
        else if (_zoneObj.activeSelf && _zone1Script._end && !_zone2Script._end) StartCoroutine(Zone2LoopIE());
        else if (_zoneObj.activeSelf && _zone2Script._end && !_zone3Script._end) StartCoroutine(Zone3LoopIE());
        else if (_mound1.activeSelf || _enemy._atack) StartCoroutine(SandAtackIE());
        else if (!_mound3.activeSelf) StartCoroutine(Sand1LoopIE());
        else if (!_mound2.activeSelf && _mound3.activeSelf) StartCoroutine(Sand2LoopIE());
        else if (!_mound1.activeSelf && _mound2.activeSelf) StartCoroutine(Sand3LoopIE());
    }
    IEnumerator MenuMusic()
    {
        _music = _menuMusic;
        FadeIn();
        yield return new WaitWhile(() => _menuScript._menu);
        FadeOut();
        Start();
    }
    IEnumerator Zone1LoopIE()
    {
        _music = _zonePart1;
        FadeIn();
        yield return new WaitWhile(() => !_zone1Script._end && !_menuScript._menu);
        FadeOut();
        Start();
    }
    IEnumerator Zone2LoopIE()
    {
        _music = _zonePart2;
        FadeIn();
        yield return new WaitWhile(() => !_zone2Script._end && !_menuScript._menu);
        FadeOut();
        Start();
    }
    IEnumerator Zone3LoopIE()
    {
        _music = _zonePart3;
        FadeIn();
        yield return new WaitWhile(() => !_zone3Script._end && !_menuScript._menu);
        FadeOut();
        Start();
    }


    IEnumerator SandAtackIE()
    {
        _music = _sandEnemy;
        FadeIn();
        if (_enemy._atack) yield return new WaitWhile(() => _enemy._atack && !_menuScript._menu && !_zoneObj.activeSelf);
        else yield return new WaitWhile(() => !_zoneObj.activeSelf && !_menuScript._menu);
        FadeOut();
        Start();
    }
    IEnumerator Sand3LoopIE()
    {
        _music = _sandPart3;
        FadeIn();
        yield return new WaitWhile(() => !_menuScript._menu && !_zoneObj.activeSelf && !_mound1.activeSelf && !_enemy._atack);
        FadeOut();
        Start();
    }
    IEnumerator Sand2LoopIE()
    {
        _music = _sandPart2;
        FadeIn();
        yield return new WaitWhile(() => !_menuScript._menu && !_zoneObj.activeSelf && !_mound2.activeSelf && !_enemy._atack);
        FadeOut();
        Start();
    }
    IEnumerator Sand1LoopIE()
    {
        _music = _sandPart1;
        FadeIn();
        yield return new WaitWhile(() => !_menuScript._menu && !_zoneObj.activeSelf && !_mound3.activeSelf && !_enemy._atack);
        FadeOut();
        Start();
    }

    void FadeOut()
    {
        FadeP();
        MusicFadeOut _fade = _music.gameObject.AddComponent<MusicFadeOut>();
        _fade._music = _music;
    }
    void FadeP()
    {
        if (!_music.gameObject.GetComponent<MusicFadeIn>())
        {
            MusicFadeIn _x = _music.gameObject.GetComponent<MusicFadeIn>();
            Destroy(_x);
        }
        if (!_music.gameObject.GetComponent<MusicFadeOut>())
        {
            MusicFadeOut _x = _music.gameObject.GetComponent<MusicFadeOut>();
            Destroy(_x);
        }
    }
    void FadeIn()
    {
        FadeP();
        MusicFadeIn _fade = _music.gameObject.AddComponent<MusicFadeIn>();
        _fade._music = _music;
    }
}