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

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        if (_zoneObj.activeSelf)
        {
            StartCoroutine(Zone1LoopIE());
        }
        else
        {
            if (_mound1.activeSelf)
            {
                StartCoroutine(SandAtackIE());
            }
            else if (!_mound1.activeSelf && _mound2.activeSelf)
            {
                StartCoroutine(Sand3LoopIE());
            }
            else if (!_mound2.activeSelf && _mound3.activeSelf)
            {
                StartCoroutine(Sand2LoopIE());
            }
            else if (!_mound3.activeSelf)
            {
                StartCoroutine(Sand1LoopIE());
            }
        }
    }
    IEnumerator Zone1LoopIE()
    {
        yield return new WaitWhile(() => _menuScript._menu);
        FadeInIE(_zonePart1);
        yield return new WaitWhile(() => !_zone1Script._end && !_menuScript._menu);
        if (_menuScript._menu)
        {
            FadeOutIE(_zonePart1);
            StartCoroutine(Zone1LoopIE());
            yield break;
        }
        FadeOutIE(_zonePart1);
        StartCoroutine(Zone2LoopIE());
    }
    IEnumerator Zone2LoopIE()
    {
        yield return new WaitWhile(() => _menuScript._menu);
        FadeInIE(_zonePart2);
        yield return new WaitWhile(() => !_zone2Script._end && !_menuScript._menu);
        if (_menuScript._menu)
        {
            FadeOutIE(_zonePart2);
            StartCoroutine(Zone2LoopIE());
            yield break;
        }
        FadeOutIE(_zonePart2);
        StartCoroutine(Zone3LoopIE());
    }
    IEnumerator Zone3LoopIE()
    {
        yield return new WaitWhile(() => _menuScript._menu);
        FadeInIE(_zonePart3);
        yield return new WaitWhile(() => !_zone3Script._end && !_menuScript._menu);
        if (_menuScript._menu)
        {
            FadeOutIE(_zonePart3);
            StartCoroutine(Zone3LoopIE());
            yield break;
        }
        FadeOutIE(_zonePart3);
        StartCoroutine(Start());
    }
    IEnumerator SandAtackIE()
    {
        yield return new WaitWhile(() => _menuScript._menu);
        if (_enemy._atack || _mound1.activeSelf)
        {
            FadeInIE(_sandEnemy);
            if (_enemy._atack)
            {
                yield return new WaitWhile(() => _enemy._atack && !_menuScript._menu && !_zoneObj.activeSelf);
                if(_menuScript._menu)
                {
                    FadeOutIE(_sandEnemy);
                    StartCoroutine(SandAtackIE());
                    yield break;
                }
            }
            else
            {
                yield return new WaitWhile(() => !_zoneObj.activeSelf && !_menuScript._menu);
                if (_menuScript._menu)
                {
                    FadeOutIE(_sandEnemy);
                    StartCoroutine(SandAtackIE());
                    yield break;
                }
            }
        }
        FadeOutIE(_sandEnemy);
        StartCoroutine(Start());
    }
    IEnumerator Sand3LoopIE()
    {
        yield return new WaitWhile(() => _menuScript._menu);
        FadeInIE(_sandPart2);
        yield return new WaitWhile(() => !_menuScript._menu && !_zoneObj.activeSelf && !_mound1.activeSelf);
        if (_menuScript._menu)
        {
            FadeOutIE(_sandPart3);
            StartCoroutine(Zone3LoopIE());
            yield break;
        }
        FadeOutIE(_sandPart3);
        StartCoroutine(Start());
    }
    IEnumerator Sand2LoopIE()
    {
        yield return new WaitWhile(() => _menuScript._menu);
        FadeInIE(_sandPart2);
        yield return new WaitWhile(() => !_menuScript._menu && !_zoneObj.activeSelf && !_mound2.activeSelf);
        if (_menuScript._menu)
        {
            FadeOutIE(_sandPart2);
            StartCoroutine(Zone2LoopIE());
            yield break;
        }
        FadeOutIE(_sandPart2);
        StartCoroutine(Start());
    }
    IEnumerator Sand1LoopIE()
    {
        yield return new WaitWhile(() => _menuScript._menu);
        FadeInIE(_sandPart1);
        yield return new WaitWhile(() => !_menuScript._menu && !_zoneObj.activeSelf && !_mound3.activeSelf);
        if (_menuScript._menu)
        {
            FadeOutIE(_sandPart1);
            StartCoroutine(Zone1LoopIE());
            yield break;
        }
        FadeOutIE(_sandPart1);
        StartCoroutine(Start());
    }


    IEnumerator FadeInIE(AudioSource _music)
    {
        while (_music.volume < 1)
        {
            _music.volume += 1f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _sandPart1.Play();
    }
    IEnumerator FadeOutIE(AudioSource _music)
    {
        while (_music.volume > 0)
        {
            _music.volume -= 1f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _sandPart1.Pause();
    }
}