using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena2 : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public string Cena;


    public void OnTriggerEnter2D(Collider2D collision)
    {

        StartCoroutine(LoadLevel("Final2"));


    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
