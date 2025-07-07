using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] public int giveScore;
    [SerializeField] private ParticleSystem DestroyedParticle;
    private ParticleSystem ParticleInstance;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private CinemachineImpulseSource impulseSource;

    ScoreManager scoreManager;
    BarManager barManager;

    private void Start()
    {
        //GetScoreGiven()
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        barManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BarManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        //giveScore = GetComponent<int>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            spriteRenderer.material.color = Color.red;
            StartCoroutine(waitBeforeChangeColor(0.1f));
            health--;
            if (health <= 0)
            {
                GetScoreGiven();
                barManager.addEnergy(1);
                scoreManager.UpdateScore(giveScore);
                CameraShakeManager.instance.CameraShake(impulseSource, 0.2f);
                summonParticle();
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("SpecialBullet"))
        {
            GetScoreGiven();
            barManager.addEnergy(1);
            scoreManager.UpdateScore(giveScore);
            CameraShakeManager.instance.CameraShake(impulseSource, 0.2f);
            summonParticle();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ult"))
        {
            CameraShakeManager.instance.CameraShake(impulseSource, 0.2f);
            GetScoreGiven();
            scoreManager.UpdateScore(giveScore);
            summonParticle();
            Destroy(gameObject);
        }
        
    }

    public int GetScoreGiven()
    {
        return giveScore;
    }

    private void summonParticle()
    {
        ParticleInstance = Instantiate(DestroyedParticle, transform.position, Quaternion.identity);
    }

    IEnumerator waitBeforeChangeColor(float duration)
    {
        yield return new WaitForSeconds(duration);
        spriteRenderer.material.color = originalColor;
    }

    IEnumerator waitBeforeDestroyed(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
