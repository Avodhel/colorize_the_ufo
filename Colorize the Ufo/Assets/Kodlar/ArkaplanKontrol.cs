using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArkaplanKontrol : MonoBehaviour
{ //!!önemli: arkaplan kaydirma için arayüzde arkaplani duplicate edip arkaplana ekledik. (parent-child hiyerarşisi)

    public float kaymaHizi; //arkaplanın y eksenindeki kayma hızı
    public float tileSizeY; // y ekseninde ne kadar birimlik bir alanda kayma işlemi gerçekleşecek.
    private Vector2 baslangicPoz; //arkaplanımın başlangıç pozisyonu

    void Start()
    {
        baslangicPoz = transform.position; //arkaplanımızın pozisyonu
    }

    void Update()
    {
        float yeniPoz = Mathf.Repeat(Time.time * kaymaHizi, tileSizeY); //yeni pozisyonun belirlenmesi
        transform.position = baslangicPoz + Vector2.down * yeniPoz; //arkaplanımızın yeni pozisyona göre güncellenmesi
    }
}
