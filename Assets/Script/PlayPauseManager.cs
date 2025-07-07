using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseManager : MonoBehaviour
{
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Image PauseTitle;
    [SerializeField] private GameObject BarrierPanel;

    public Animator MenuPanelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        BarrierPanel.gameObject.SetActive(false);
        PauseTitle.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        BarrierPanel.gameObject.SetActive(true );
        PauseButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);
        PauseTitle.gameObject.SetActive(true) ;
        Time.timeScale = 0.0f;
        MenuPanelAnimator.SetBool("IsPaused", true);
    }

    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(MenuPanelExit());
    }

    IEnumerator MenuPanelExit()
    {
        MenuPanelAnimator.SetBool("IsPaused", false);
        yield return new WaitForSecondsRealtime(0.3f);

        BarrierPanel.gameObject.SetActive(false );
        PauseButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);
        PauseTitle.gameObject.SetActive(false);

    }
    
}
