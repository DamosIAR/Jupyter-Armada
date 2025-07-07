using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    public static HitPause instance;

    private bool waiting;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void hitStop(float duration)
    {
        if (waiting) return;
        Time.timeScale = 0.0f;
        StartCoroutine(wait(duration));
    }

    IEnumerator wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
