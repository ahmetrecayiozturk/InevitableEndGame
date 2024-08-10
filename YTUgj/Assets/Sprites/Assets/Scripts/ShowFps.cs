using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShowFps : MonoBehaviour
{
    public TMP_Text m_Text; // Unity arayüzünde metin alanýný baðlamak için kullanýlýr.

    [SerializeField] private float delay;
    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            m_Text.SetText("FPS : " + (int)(1 / Time.deltaTime));

            timer = delay;
        }

    }
}
