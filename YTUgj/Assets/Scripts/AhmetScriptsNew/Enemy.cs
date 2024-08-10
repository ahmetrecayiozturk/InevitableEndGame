using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 50f; // Düşmanın maksimum canı
    private float currentHealth; // Düşmanın mevcut canı
    public float moveSpeed = 15f; // Hareket hızı
    private Transform player; // Oyuncu referansı
    public float damage = 100f;
    public float timeToAdd = 20f;
    private PlayerController player_controller; // PlayerController referansı
    private bool isFlipped;
    public GameObject deathEffect;
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player_controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        Flip();
        // Düşman öldü mü kontrol et
        if (currentHealth <= 0)
        {
            Die();
        }

        // Oyuncuya doğru hareket et
        MoveTowardsPlayer();
    }

    void Die()
    {
        // Düşman öldüğünde yapılacak işlemler
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.45f);
        AddScore();
        AddTime();
        this.gameObject.SetActive(false);
    }

    void MoveTowardsPlayer()
    {
        // Oyuncuya doğru hareket et
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void TakeDamage(float damage)
    {
        Debug.Log("Damage Çalışıyorr");
        // Düşmanın canını azalt
        currentHealth -= damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer Player düşman objesiyle temas ederse
        if (other.CompareTag("PlayerBullet"))
        {
            Debug.Log("PlayerBullet Enemey'e temas ettii");
            // Zamanı azalt
            TakeDamage(damage);
        }
    }

    void AddTime()
    {
        Debug.Log("Added Time");
        player_controller.currentTime += timeToAdd;
    }
    void AddScore(){
        player_controller.score += 1;
        Debug.Log("Added Score"+player_controller.score);
    }
    public void Flip()
    {
        // Fare konumunu dünya koordinatlarına dönüştür.
        // Silahın konumunu ve fare konumu arasındaki yönü bul.
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Vector2 lookDirection = (Vector2)playerObject.transform.position - (Vector2)transform.position;

            // Yön vektöründen açıyı hesapla.
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            if (angle < -90 && angle > -180) // 3. bolge
            {
                if (!isFlipped)
                {
                    FlipEnemy();
                }
            }
            else if (angle > 90 && angle < 180) // 2. bolge
            {
                if (!isFlipped)
                {
                    FlipEnemy();
                }
            }
            else
            {
                if (isFlipped)
                {
                    FlipEnemy();
                }
            }
        }
        void FlipEnemy()
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFlipped = !isFlipped;

        }
    }
}
