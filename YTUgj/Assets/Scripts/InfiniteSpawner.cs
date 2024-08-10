using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
    public float startTimeBtwSpawn;
    private float timeBtwSpawn;

    public GameObject[] enemies;
    public Transform spawnPos;
    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            int randEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randEnemy], spawnPos.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
