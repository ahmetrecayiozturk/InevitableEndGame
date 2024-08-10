using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sound : MonoBehaviour
{
    public AudioClip shoot;

    private float timeBtwShoot = 0f;
    private AudioSource audioSource;
    
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        timeBtwShoot -= Time.deltaTime;
        if (timeBtwShoot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(shoot);
                timeBtwShoot = 0.5f;
            }
        }
        
        

    }
}
