using System.Collections;
using System.Collections.Generic;
using System.Collections;  // IEnumerator kullanabilmek için gerekli kütüphane
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public GameObject enemyPrefab; // Spawn edilecek düşmanın prefab'ı
    public float spawnRadius = 5f; // Düşmanların spawn olacağı alanın yarıçapı
    public int spawnCount = 10; // Toplam spawn edilecek düşman sayısı
    public float spawnInterval = 1f; // Düşmanların spawn edilme aralığı
    public float innerRadius = 2f; // İç bölgede spawn edilen düşmanların orta noktaya olan en yakın mesafesi
    public bool isMatched = false;
    public GameObject[] enemies;

    [SerializeField] private GameObject nextWaveActivator;

    // Start fonksiyonu, nesne oluşturulduğunda çalışır

    // Düşmanların spawn edilmesini sağlayan IEnumerator fonksiyonu
    IEnumerator SpawnEnemies()
    {
        // Belirtilen sayıda düşman spawn edene kadar döngüyü devam ettir
        for (int i = 0; i < spawnCount; i++)
        {
            // Rastgele bir spawn noktası belirle
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
            // Eğer spawn noktasının uzaklığı iç bölgede belirtilen iç yarıçapın altındaysa, noktayı iç bölgeye al
            if (spawnPosition.magnitude < innerRadius)
            {
                spawnPosition = spawnPosition.normalized * innerRadius;
            }

            // Düşmanı spawn et
            
            int randEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randEnemy], spawnPosition, Quaternion.identity);
         
            // Belirtilen aralık kadar bekle
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer Player düşman objesiyle temas ederse
        if (other.CompareTag("Player"))
        {
            Debug.Log("MatchController Enemy'e temas ettii");
            // Zamanı azalt
            StartCoroutine(SpawnEnemies());

            GetComponent<CircleCollider2D>().enabled = false;

            if (nextWaveActivator)
            {
                nextWaveActivator.SetActive(true);
            }
        }
    }

}

