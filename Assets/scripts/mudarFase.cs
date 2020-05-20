using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mudarFase : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;


    public void StageSelect()
    {
        StartCoroutine(LoadLevel());
    }
    public void Game()
    {
        StartCoroutine(LoadLevel());
    }
    public void Fase2()
    {
        StartCoroutine(LoadLevel());
    }
    public void Final()
    {
        StartCoroutine(LoadLevel());
    }
    public void Final2()
    {
        StartCoroutine(LoadLevel());
    }
    public void Menu()
    {
        StartCoroutine(LoadLevel());
    }


    IEnumerator LoadLevel([CallerMemberName] string sceneName = null)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }



}
