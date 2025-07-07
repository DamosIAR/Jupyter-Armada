using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    public Transform spawnerPoint;
    private float force;
    public float spawnInterval;
    private float resetInterval;

    [SerializeField] private float LeftMost;
    [SerializeField] private float RightMost;

    private void Awake()
    {
        resetInterval = spawnInterval;
    }

    private void Update()
    {
        if (GameStateManager.Instance.isCountdownToStart()) return;
        //(Random.Range(7f, 8f) * Time.deltaTime) * 100
        force = 5;
        Vector2 leftRight = transform.position;

        spawnInterval -= Time.deltaTime;
        if(spawnInterval <= 0)
        {
            SpawnEnemy();
            spawnInterval = resetInterval;
        }
    }

    private void SpawnEnemy()
    {
        float GetPosition = Random.Range(LeftMost, RightMost);
        Vector3 SpawnerPosition = new Vector3(GetPosition, transform.position.y);
        GameObject enemy = Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], SpawnerPosition, spawnerPoint.rotation);
        enemy.GetComponent<Rigidbody>().AddForce(spawnerPoint.up * force, ForceMode.Impulse);
        //Debug.Log(force);
    }



}
