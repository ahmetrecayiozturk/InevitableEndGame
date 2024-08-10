using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er Player d��man objesiyle temas ederse
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
