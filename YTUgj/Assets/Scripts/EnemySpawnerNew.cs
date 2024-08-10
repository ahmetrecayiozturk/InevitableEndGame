using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerNew : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnRadius = 5f;
    public int spawnCount = 10;
    public float spawnInterval = 1f;
    public float innerRadius = 2f;
    public float detectionRadius = 10f; // Oyuncuyu algılama yarıçapı
    private bool hasSpawned = false; // Düşmanlar spawn edildi mi?

    [SerializeField] private GameObject nextWaveActivator;

    void Update()
    {
        // Eğer düşmanlar henüz spawn edilmediyse
        if (!hasSpawned)
        {
            // Oyuncuyu algılama yarıçapındaki tüm collider'ları al
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

            // Her bir collider için
            foreach (Collider2D collider in colliders)
            {
                // Eğer collider bir oyuncuya aitse
                if (collider.CompareTag("Player"))
                {
                    // Düşmanları spawn et
                    StartCoroutine(SpawnEnemies());
                    // Düşmanların spawn edildiğini belirt
                    hasSpawned = true;
                    break;
                }
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
            if (spawnPosition.magnitude < innerRadius)
            {
                spawnPosition = spawnPosition.normalized * innerRadius;
            }

            int randEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randEnemy], spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }

        GetComponent<CircleCollider2D>().enabled = false;

        if (nextWaveActivator)
        {
            nextWaveActivator.SetActive(true);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
