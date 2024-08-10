using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public Transform firingPoint;

    public float fireRate;
    private float timeToFire;

    public GameObject bulletPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeToFire = fireRate;
    }
    void Update()
    {
        


    }
    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            Rigidbody2D rb = bulletPrefab.GetComponent<Rigidbody2D>();
            rb.AddForce(firingPoint.up * 6, ForceMode2D.Impulse);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {

        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToShoot)
            {
                rb.velocity = transform.up * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardTarget();
        }

        if (target != null && Vector2.Distance(target.position, transform.position) >= distanceToStop)
        {
            Shoot();
        }
    }

    void RotateTowardTarget()
    {
        Vector2 targetDirection = target.position- transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0, 0, angle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q,rotateSpeed);
    }
    void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            target = null;
        }
        else if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
