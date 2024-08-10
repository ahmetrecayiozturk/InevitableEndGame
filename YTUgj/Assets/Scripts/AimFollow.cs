using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bu kod aim isaretinin mouse takip edip kendi etrafinda donmesini sagliyor
public class AimFollow : MonoBehaviour
{
    public float rotSpeed;
    void Update()
    {
        //aimin donmesi 
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        // aimin mousu takip etmesi
        Vector2 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, aimPos, 100 * Time.deltaTime);
    }
}
