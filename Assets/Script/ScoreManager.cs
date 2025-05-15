using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //EnemyManager EnemyManager;
    private int addScore;
    private int olahscore;
    public int score;
    public TextMeshProUGUI TextScore;

    private void Start()
    {
        //EnemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        score = 0;
        TextScore.text = "Score: " + score;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        TextScore.text = "Score: " + score;
    }

}
