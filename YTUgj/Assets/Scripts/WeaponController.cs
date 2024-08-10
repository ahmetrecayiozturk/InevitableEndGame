using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator shoot;
    public Transform weaponTransform;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
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
                shoot.SetTrigger("shootPistol");
                timeBtwShoot = 0.5f;
            }
        }

    }
    private void FixedUpdate()
    {

    }
    public void AimWeapon()
    {
        // Fare konumunu dünya koordinatlarýna dönüþtür.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Silahýn konumunu ve fare konumu arasýndaki yönü bul.
        Vector2 lookDirection = mousePosition - (Vector2)transform.position;
        // Yön vektöründen açýyý hesapla.
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        bool chechTerritory = ((angle < -90 && angle > -180) || (angle > 90 && angle < 180)) ? true : false;
        if (chechTerritory)
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
        // Silahýn rotasyonunu ayarla.
        weaponTransform.rotation = Quaternion.Euler(0, 0, angle);


    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

    void firePointRotation()
    {

        // Fare konumunu dünya koordinatlarýna dönüþtür.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Silahýn konumunu ve fare konumu arasýndaki yönü bul.
        Vector2 lookDirection = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

    }
    void FlipGun()
    {
        //(degistirdim ama not dursun yinede)-x olmasinin sebebi karakterin kendiside donuyor ve bu script karakterin icinde o yuzden bu scriptin x ekseni zit oluyor bunu cozmek icin -x yaptim
        weaponTransform.localScale = new Vector3(weaponTransform.localScale.x, -weaponTransform.localScale.y, weaponTransform.localScale.z);
        isGunFlipped = !isGunFlipped;
    }
}
