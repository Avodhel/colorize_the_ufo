using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    public GameObject engel;
    public GameObject[] engelTipleri;
    public int kacAdetEngel;
    GameObject[] engeller;
    float engelDegisimZamani = 0f;
    int sayac = 0;
    public float engelHiz;
    public float engelOlusmaAraligi;
    public Color[] renkler;
    GameObject engelChildSayisi;
    public GameObject oyunuDurdurButonu;
    public Sprite oyunuDurdurSprite;
    public Sprite oyunaDevamSprite;
    public GameObject oyunDurduPanel;
    float oyunDurduTimeScale;
    public GameObject sesiKapaAcButonu;
    public Sprite sesiAcSprite;
    public Sprite sesiKapaSprite;
    //int sesAcKapaKontrol = 0;
    public Image changeColorImage;
    int ilkOynama;

    void Start()
    {
        //PlayerPrefs.DeleteAll();

        ilkKezMiOynaniyor();

        engelOlustur();
        engelChildSayisi = GameObject.Find(engel.transform.name + "(Clone)"); //engel objesini bul

        sesAcikMiKapaliMi();
    }

    void Update()
    {
        engelPozisyonBelirle();
    }

    void engelOlustur()
    {
        engeller = new GameObject[kacAdetEngel]; //kacAdetEngel'e verilen sayı kadar engel objesi
        for (int i = 0; i < engeller.Length; i++) // engeller dizisi kadar obje oluştur
        {
            engel = engelTipleri[Random.Range(0, engelTipleri.Length)]; //birden farklı engel tipi arasından seçim
            engeller[i] = Instantiate(engel, new Vector2(-10, -10), Quaternion.identity); // engel objelerinin oluşturulması
            Rigidbody2D fizikEngel = engeller[i].AddComponent<Rigidbody2D>(); //engelimize koddan rigidbody ekledik.
            fizikEngel.bodyType = RigidbodyType2D.Kinematic; //!!!dynamic olunca engeller savruluyor
            fizikEngel.gravityScale = 0; //oluşan engellerin düşmemesi için 
            fizikEngel.velocity = new Vector2(0, -engelHiz); // engellerimizin hareket etmesi için.
        }
    }

    void engelPozisyonBelirle() // oluşturulan engellerin random pozisyonlarda oluşması için metot
    {
        engelDegisimZamani += Time.deltaTime;
        if (engelDegisimZamani > engelOlusmaAraligi) // belirlenen saniyede bir bu koşula girecek ve engelleri oluşturacak.
        {
            engelDegisimZamani = 0;
            float xEkseniDegisim = Random.Range(-10f, -2f); // x eksenini random olarak değiştirerek engellerin random bir konumda gelmesini sağlıyoruz.
            engeller[sayac].transform.position = new Vector3(xEkseniDegisim, 7f); // engellerimizin oluşum alanını belirliyoruz.
                                                                                  /*!!! Vector3 iki tane değer de alabilir.*/
            sayac++;
            if (sayac >= engeller.Length) // dizideki obje sayısına ulaştığımda
            {
                sayac = 0; // sayacı sıfırla
            }

            if (engeller[sayac].transform.position.y <= -6f) // engel sahneden çıktığında rengini ve triggerını değiştirmek için koşul
            {
                for (int a = 0; a < engelChildSayisi.transform.childCount; a++)
                {
                    if (engeller[sayac].transform.GetChild(a).tag == "randomEngelTag")
                    {
                        engeller[sayac].transform.GetChild(a).GetComponent<SpriteRenderer>().color = renkler[Random.Range(0, renkler.Length)]; //renk degistir

                        if (engeller[sayac].transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger == true) //istriggeri açıksa 
                        {
                            engeller[sayac].transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger = false; //kapat
                        }
                    }
                }
            }
        }
    }

    public void oyunuDurdurveDevamEt() //oyunu durdur butonuna basıldığında
    {
        if (Time.timeScale >= 1) //oyun devam ediyorsa
        {
            oyunDurduTimeScale = Time.timeScale; //kalınan hız bilgisini al
            Time.timeScale = 0; // oyunu durdur
            oyunuDurdurButonu.GetComponent<Image>().sprite = oyunaDevamSprite;
            oyunDurduPanel.SetActive(true);
            AudioListener.pause = true; //sesleri kapat
            changeColorImage.enabled = false; // oyun durduğunda ufonun rengi değiştirilemesin
        }
        else if (Time.timeScale == 0) // oyun durmuşsa
        {
            Time.timeScale = oyunDurduTimeScale; //oyuna kalınan hızdan devam et
            oyunuDurdurButonu.GetComponent<Image>().sprite = oyunuDurdurSprite;
            oyunDurduPanel.SetActive(false);
            AudioListener.pause = false;//sesi tekrar ac
            changeColorImage.enabled = true; // ufo rengi değiştirmeyi tekrar aktif et
        }
    }

    public void sesleriKapatveAc()
    {
        if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 0) //ses açıksa
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiKapaSprite;
            AudioListener.volume = 0f; //sesi kapat
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 1);
        }
        else if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 1) //ses kapalıysa
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiAcSprite;
            AudioListener.volume = 1f; //sesi ac
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 0);
        }
    }

    void sesAcikMiKapaliMi() //restart sonrası ses acik veya kapali sprite sorununu çözen fonksiyon
    {
        if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 0)
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiAcSprite;
            AudioListener.volume = 1f; //sesi ac
        }
        else if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 1)
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiKapaSprite;
            AudioListener.volume = 0f; //sesi kapat
        }
    }

    void ilkKezMiOynaniyor() //oyun ilk kez oynanıyorsa tutorial gözükmesi için fonksiyon
    {
        ilkOynama = PlayerPrefs.GetInt("ilkKezMiOynaniyor");

        if (ilkOynama <= 2)
        {
            ilkOynama = ilkOynama + 1;
            PlayerPrefs.SetInt("ilkKezMiOynaniyor", ilkOynama);
        }
    }

}
