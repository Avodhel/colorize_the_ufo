using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput; //mobil entegrasyon için kütüphane
using UnityEngine.EventSystems;

public class UfoKontrol : MonoBehaviour
{

    Rigidbody2D fizik;
    float horizontal = 0f;
    public float karakterHiz;
    public float maxX;
    public float minX;
    Vector3 vec;
    SpriteRenderer spriteRenderer;
    public Color[] renkler;
    int index = 1;
    public Text puanText;
    public Text puan2Text;
    public Text enYuksekPuanText;
    int puan = 0;
    int enYuksekPuan = 0;
    public GameObject oyunBittiPanel;
    public GameObject oyunuDurdurButonu;
    public Image canBari;
    public Image enerjiBari;
    public Color[] canBariRenk;
    public Color[] enerjiBariRenk;
    public GameObject patlama;
    public GameObject ufoMotor;
    int oyunBittiSayac = 0;

    void Start()
    {
        fizik = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        puanText.text = "Score: " + puan;
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit"); // en yüksek puan bilgimi çekiyorum.

        ufoMotor.SetActive(true); //ufo motoru görünür yap

        //PlayerPrefs.DeleteAll();
    }

    void Update()
    {
        ufoRenkDegistir();
    }

    void FixedUpdate()
    {
        ufoHareket();
        enerjiBariKontrol();
        canBariKontrol();
    }

    void ufoHareket()
    {
//#if UNITY_WEBGL
        horizontal = Input.GetAxisRaw("Horizontal"); //normal hareket
//#elif UNITY_ANDROID
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal"); //mobilde hareket
//#endif
        /*hareket tuşuna basıldığında getaxisraw 0'dan 1 olur getaxis ise 0.1'den 0.2*/
        vec = new Vector3(horizontal * karakterHiz, fizik.velocity.y, 0); // sırasıyla parantez içi: x ekseninde 10 hızında koş | y eksenindeki hızım neyse o olsun |
        fizik.velocity = vec;

        fizik.position = new Vector3( // ufonun ekranın dışına çıkmaması için sınır koordinatlarını belirliyoruz.
        Mathf.Clamp(fizik.position.x, minX, maxX),
        transform.position.y
        );
    }

    void ufoRenkDegistir()
    {
//#if UNITY_WEBGL
        if (Input.GetButtonDown("Jump"))
        {
            index += 1;
            if (index == renkler.Length + 1)
            {
                index = 1;
            }
            spriteRenderer.color = renkler[index - 1];
        }
//#elif UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            index += 1;
            if (index == renkler.Length + 1)
            {
                index = 1;
            }
            spriteRenderer.color = renkler[index - 1];
        }
//#endif
    }

    void enerjiBariKontrol()
    {
        enerjiBari.fillAmount -= 0.00011f; //ufo ilerledikçe enerji barı azalıyor
        if (enerjiBari.fillAmount == 0) //enerji bari sifirlaninca oyun bitsin
        {
            oyunBitti();
        }

        if (enerjiBari.fillAmount >= 0.65f)
        {
            enerjiBari.color = enerjiBariRenk[0];
        }
        else if (enerjiBari.fillAmount >= 0.33f)
        {
            enerjiBari.color = enerjiBariRenk[1];
        }
        else
        {
            enerjiBari.color = enerjiBariRenk[2];
        }
    }

    void canBariKontrol()
    {
        if (canBari.fillAmount == 0) //can bari sifirlaninca oyun bitsin
        {
            oyunBitti();
        }

        if (canBari.fillAmount >= 0.65f)
        {
            canBari.color = canBariRenk[0];
        }
        else if (canBari.fillAmount >= 0.33f)
        {
            canBari.color = canBariRenk[1];
        }
        else
        {
            canBari.color = canBariRenk[2];
        }
    }

    void OnCollisionEnter2D(Collision2D col) // geçirgen olmayan bir yüzeye temas edildiğinde çalışır.
    {
        //ziplamaKontrol = true; // zıpladıktan sonra karakter yere değdiğinde zıplama tekrar aktif oluyor.

        if (col.transform.tag == "engellerTag") // ufo engellere çarptığında
        {
            for (int i = 0; i < col.transform.childCount; i++) //bütün alt nesnelerine bak
            {
                if (col.transform.GetChild(i).tag == "randomEngelTag") //tagi "randomengeltag" ise
                {
                    if (col.transform.GetChild(i).GetComponent<SpriteRenderer>().color == transform.GetComponent<SpriteRenderer>().color) //engel ile topun rengi aynı ise
                    {
                        col.transform.GetChild(i).GetComponent<BoxCollider2D>().isTrigger = true; //trigger ını aç ki top geçebilsin.
                    }
                }
            }
        }

        if (col.transform.tag == "oyunBittiSinirTag") // eğer ufo ezildiyse
        {
            oyunBitti();
        }

        if (col.transform.tag == "ucanCisimTag")
        {
            if (col.transform.GetComponent<SpriteRenderer>().color == transform.GetComponent<SpriteRenderer>().color)
            {
                FindObjectOfType<SesKontrol>().sesOynat("Puan"); //puan sesini oynat
                Destroy(col.gameObject);
                puan += 1;
                puanText.text = "Score: " + puan;
                enerjiBari.fillAmount += 0.005f; //cisim toplanınca enerji artıyor
            }
            else
            {
                FindObjectOfType<SesKontrol>().sesOynat("Carpma"); //carpma sesini oynat
                Destroy(col.gameObject);
                canBari.fillAmount -= 0.25f;
            }
        }

        if (col.transform.tag == "meteorTag")
        {
            Destroy(col.gameObject);
            oyunBitti();
        }


        if (col.transform.tag == "canVerenCisimTag")
        {
            if (canBari.fillAmount < 1) //can full değilse
            {
                FindObjectOfType<SesKontrol>().sesOynat("CanveEnerji"); //can ve enerji sesini oynat
                Destroy(col.gameObject);
                canBari.fillAmount += 0.5f;
            }
            else
            {
                col.transform.GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }

        if (col.transform.tag == "enerjiVerenCisimTag")
        {
            if (enerjiBari.fillAmount < 1) //enerji full değilse
            {
                FindObjectOfType<SesKontrol>().sesOynat("CanveEnerji"); //can ve enerji sesini oynat
                Destroy(col.gameObject);
                enerjiBari.fillAmount += 0.2f;
            }
            else
            {
                col.transform.GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }

        if (col.transform.tag == "yavaslatanCisimTag")
        {
            if (Time.timeScale > 1) //hiz 1'den büyükse
            {
                FindObjectOfType<SesKontrol>().sesOynat("Yavaslatma"); //yavaslatma sesini oynat
                Destroy(col.gameObject);
                Time.timeScale = 1;
            }
            else
            {
                col.transform.GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "randomEngelTag")
        {
            FindObjectOfType<SesKontrol>().sesOynat("EngelPuan"); //EngelPuan sesini oynat
            puan += 5;
            puanText.text = "Score: " + puan;
            enerjiBari.fillAmount += 0.015f; //engel aşılınca enerji artıyor
        }
    }

    void anaMenuyeDon()
    {
        SceneManager.LoadScene(0);
    }

    public void yenidenBasla()
    {
        oyunBittiPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    void oyunBitti()
    {
        ufoMotor.SetActive(false); //ufo motoru görünmez yap
        Instantiate(patlama, gameObject.transform.localPosition, Quaternion.identity); // patlama efekti oluştur
        FindObjectOfType<SesKontrol>().sesOynat("UfoPatlama"); //ufo patlama sesini oynat
        transform.gameObject.SetActive(false);
        oyunBittiPanel.SetActive(true);
        puan2Text.text = "Score: " + puan;

        if (puan > enYuksekPuan) // en yüksek puan için koşul
        {
            enYuksekPuan = puan;
            PlayerPrefs.SetInt("enYuksekPuanKayit", enYuksekPuan); // en yüksek puanı kayıtlı tutuyoruz.
        }
        enYuksekPuanText.text = "High Score: " + PlayerPrefs.GetInt("enYuksekPuanKayit", enYuksekPuan); // en yuksek puanın gösterilmesi

        enerjiBari.fillAmount = 0; //enerji barını sıfırla
        canBari.fillAmount = 0; //can barini sıfırla

        oyunuDurdurButonu.SetActive(false);

        /*Reklam göster*/
        oyunBittiSayac = PlayerPrefs.GetInt("oyunBittiSayac");
        oyunBittiSayac++;
        PlayerPrefs.SetInt("oyunBittiSayac", oyunBittiSayac);
        Debug.Log(oyunBittiSayac);

        if (PlayerPrefs.GetInt("oyunBittiSayac") == 3) //3 kere oyun bittiğinde reklam göster
        {
#if UNITY_ANDROID
            GameObject.FindGameObjectWithTag("reklamKontrolTag").GetComponent<ReklamKontrol>().reklamiGoster();
#endif
            PlayerPrefs.SetInt("oyunBittiSayac", 0);
        }
    }
}
