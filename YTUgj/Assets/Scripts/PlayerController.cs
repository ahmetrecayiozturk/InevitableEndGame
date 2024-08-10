using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector2 movement;
    public Rigidbody2D rb;
    public float moveSpeed;
    public float activeSpeed;
    public float dashSpeed;
    public float dashLenght = 5f, dashCooldown = 1f;
    public float dashCounter;
    public float dashCoolCounter;
    public float maxTime = 80f; // Maksimum zaman
    public float currentTime; // Geçen zaman
    public float timeDecreaseSpeed = 1f; // Zaman azalma hızı
    public float damageAmount = 10f; // Zarar miktarı
    private float timer = 1f; // Timer
    public Text TimeText;
    private bool isGameOver = false;
    public GameObject gameOverPanel;
    public int score = 0;

    public Animator animDoctor;
    private float f1, f2;
    private bool checkIsRunning;
    private Vector2 mousePosition;
    public Transform doctorTransform;
    private float timeBtwShoot = 0f;
    private bool isFlipped;

    void Start()
    {
        currentTime = 80f;
        gameOverPanel.SetActive(false); // gameOverPanel'i başlangıçta kapalı yap
        activeSpeed = moveSpeed;
    }
    void Update()
    {
        timeBtwShoot -= Time.deltaTime;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        f1 = movement.x = Input.GetAxisRaw("Horizontal");
        f2 = movement.y = Input.GetAxisRaw("Vertical");
        checkIsRunning = (Mathf.Abs(f1) > 0 || Mathf.Abs(f2) > 0) ? true : false;
        if(currentTime >80){
            currentTime = 80;
        }

        // Timer'ı azalt
        timer -= Time.deltaTime;
        // Eğer timer 0'a ulaşırsa
        if (timer <= 0)
        {
            // Zamanı azalt
            currentTime -= timeDecreaseSpeed;
            UpdateTimeText();
            // Timer'ı sıfırla
            timer = 1f;
        }
        // Oyuncu öldü mü kontrol et
        if (currentTime <= 0)
        {
            Die();
        }
        if (timeBtwShoot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentTime -= 3;
                timeBtwShoot = 0.8f;
            }
        }
    }
    private void FixedUpdate()
    {
        FlipPlayer();
        animDoctor.SetBool("isRun", checkIsRunning);
        if (!isGameOver)
        {
            // Player'ın yatay ve dikey hareketini al
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            // Hareket vektörünü oluştur
            Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

            // Player'ı hareket ettir
            GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Oyun bitti! Yeniden başlatılıyor...");
                RestartGame();
            }
        }
    }
    void Die()
    {
        gameOverPanel.SetActive(true); // gameOverPanel'i aktifleştir
        TimeText.text = "Time's up! Press R to try again"; // TimeText'i güncelle
        isGameOver = true;
        // Oyuncu öldüğünde yapılacak işlemler
        // Bu kısımda oyunu yeniden başlatan veya oyunu bitiren kodlar yer alır
    }
    void UpdateTimeText()
    {
        TimeText.text = "TIME LEFT: " + currentTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer Player düşman objesiyle temas ederse
        if (other.CompareTag("Enemy1"))
        {
            // Zamanı azalt
            currentTime -= damageAmount;
        }
        if (other.CompareTag("EnemyBullet"))
        {
            // Zamanı azalt
            currentTime -= damageAmount;
        }
    }
    void RestartGame()
    {
        // Aktif olan sahneyi tekrar yükle
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void FlipPlayer()
    {
        // Fare konumunu dünya koordinatlarına dönüştür.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Silahın konumunu ve fare konumu arasındaki yönü bul.
        Vector2 lookDirection = mousePosition - (Vector2)transform.position;

        // Yön vektöründen açıyı hesapla.
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (angle < -90 && angle > -180)//3. bolge
        {
            if (!isFlipped)
            {
                FlipDoctor();
            }
        }
        else if (angle > 90 && angle < 180)// 2. bolge
        {
            if (!isFlipped)
            {
                FlipDoctor();
            }
        }
        else
        {
            if (isFlipped)
            {
                FlipDoctor();
            }

        }
        // doctorin transformunu x ekseninde degistiriyor
        void FlipDoctor()
        {
            doctorTransform.localScale = new Vector3(-doctorTransform.localScale.x, doctorTransform.localScale.y, doctorTransform.localScale.z);
            //doctorTransform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFlipped = !isFlipped;
        }
    }
}

