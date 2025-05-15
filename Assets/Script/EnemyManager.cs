using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] public int giveScore;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    ScoreManager scoreManager;
    BarManager barManager;

    private void Start()
    {
        //GetScoreGiven();
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        barManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BarManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;
        //giveScore = GetComponent<int>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                Debug.Log(GetScoreGiven());
                GetScoreGiven();
                barManager.addEnergy(1);
                scoreManager.UpdateScore(giveScore);
                Destroy(gameObject);
                //scoreManager.UpdateScore(giveScore);
                //StartCoroutine(waitBeforeDestroyed());
            }
        }
        else if (other.CompareTag("Ult"))
        {
            GetScoreGiven();
            scoreManager.UpdateScore(giveScore);
            Destroy(gameObject);
        }
        
    }

    public int GetScoreGiven()
    {
        return giveScore;
    }

    IEnumerator waitBeforeDestroyed()
    {
        spriteRenderer.material.color = Color.black;
        barManager.addEnergy(1);
        yield return new WaitForSeconds(0.5f);
        scoreManager.UpdateScore(giveScore);
        Destroy(gameObject);
    }

}
