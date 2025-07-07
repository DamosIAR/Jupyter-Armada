using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverandRetry : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject BarrierPanel;
    [SerializeField] private TextMeshProUGUI ScoreToShow;

    private int Score;

    public static GameOverandRetry instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        BarrierPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void GameOverFadeIn()
    {
        Score = ScoreManager.instance.GetScore();
        ScoreToShow.text = "Score: " + Score;
        //if (!GameStateManager.Instance.IsGameOver())  return;
        BarrierPanel.SetActive(true);
        GameOverPanel.SetActive(true);
        animator.SetBool("GameOver", true);
    }
}
