using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;

    public void changeScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame(string sceneName)
    {
        StartCoroutine(waitBeforeChangeScene(sceneName));
    }

    IEnumerator waitBeforeChangeScene(string sceneName)
    {
        animator.SetBool("HasEnter", false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
