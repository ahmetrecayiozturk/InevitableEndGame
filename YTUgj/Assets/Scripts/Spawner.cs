using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Oluþturulacak nesneler
    public GameObject[] nesnePrefabrikalari;

    // Oluþturma yarýçapý (geniþlik)
    public float olusturmaGenisligi = 10.0f;

    // Oluþturma yarýçapý (yükseklik)
    public float olusturmaYuksekligi = 5.0f;

    // Oluþturulacak nesne sayýsý
    public int olusturmaAdedi = 10;

    void Start()
    {
        NesneleriOlustur();
    }

    void NesneleriOlustur()
    {
        for (int i = 0; i < olusturmaAdedi; i++)
        {
            Vector2 rastgeleKonum = new Vector2(Random.Range(-olusturmaGenisligi / 2.0f, olusturmaGenisligi / 2.0f), Random.Range(-olusturmaYuksekligi / 2.0f, olusturmaYuksekligi / 2.0f));

            int rastgeleIndeks = Random.Range(0, nesnePrefabrikalari.Length);

            Instantiate(nesnePrefabrikalari[rastgeleIndeks], rastgeleKonum, Quaternion.identity);
        }
    }
}