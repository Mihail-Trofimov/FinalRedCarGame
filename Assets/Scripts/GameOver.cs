using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _timeWait = 0.15f;
    [SerializeField] private float _timeMinus = 0.15f;
    private void Start()
    {
        Cursor.visible = true;
        StartCoroutine(AudioIE());
    }
    IEnumerator AudioIE()
    {
        yield return new WaitForSeconds(_timeWait);
        _sound.Play();
        yield return new WaitForSeconds(_sound.clip.length - _timeMinus);
        _music.Play();
    }
    public void OnMainMenu()
    {
        StartCoroutine(Menu());
    }
    IEnumerator Menu()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(0);
    }
    public void OnExit()
    {
        StartCoroutine(Exit());
    }
    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.15f);
        Application.Quit();
    }
}
