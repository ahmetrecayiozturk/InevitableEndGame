using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootgunController : MonoBehaviour
{
  
    public Animator shoot;
    public Transform weaponTransform;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public int numberOfBullets = 5; // Shotgun'da atýlacak mermi sayýsý
    public float spreadAngle = 15f; // Mermilerin daðýlacaðý açý
    private float timeBtwShoot = 0f;
    private bool isFlipped;
    private Vector2 mousePosition;
    private bool isGunFlipped = false;

    void Update()
    {
        timeBtwShoot -= Time.deltaTime;
        firePointRotation();
        AimWeapon();
        if (timeBtwShoot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                shoot.SetTrigger("shootShotgun");
                timeBtwShoot = 0.5f;
            }
        }

    }

    public void AimWeapon()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        bool checkTerritory = ((angle < -90 && angle > -180) || (angle > 90 && angle < 180)) ? true : false;
        if (checkTerritory)
        {
            if (!isGunFlipped)
            {
                FlipGun();
            }
        }
        else
        {
            if (isGunFlipped)
            {
                FlipGun();
            }
        }
        weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        float startAngle = -spreadAngle / 2;
        float angleIncrement = spreadAngle / (numberOfBullets - 1);
        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentAngle = startAngle + angleIncrement * i;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, currentAngle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation * firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void firePointRotation()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FlipGun()
    {
        weaponTransform.localScale = new Vector3(weaponTransform.localScale.x, -weaponTransform.localScale.y, weaponTransform.localScale.z);
        isGunFlipped = !isGunFlipped;
    }
}


