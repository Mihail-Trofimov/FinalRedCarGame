using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
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
