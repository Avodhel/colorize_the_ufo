using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput; //library for mobile entegration

public class UfoControl : MonoBehaviour
{
    [Header("Ufo Speed")]
    [SerializeField]
    private float speed = 3.3f;

    [Header("Movement Border")]
    [SerializeField]
    private float maxX = 3.1f;
    [SerializeField]
    private float minX = -3.1f;

    [Header("Colors")]
    //[SerializeField]
    //private Color[] colors;
    [SerializeField]
    private Color[] healthBarColor;
    [SerializeField]
    private Color[] energyBarColor;

    [Header("UI Objects")]
    [SerializeField]
    private Text pointText;
    [SerializeField]
    private Text point2Text;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject pauseGameButton;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image energyBar;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject ufoEnginePrefab;

    private Rigidbody2D rb2d;
    private Vector3 vec3;
    private SpriteRenderer spriteRenderer;
    private GameObject gameControl;
    private Color[] colors;

    private float horizontal = 0f;
    private int index = 1;
    private int point = 0;
    private int enYuksekPuan = 0;
    private int gameOverTimer = 0;
    private bool moveControl = true;

    private void Awake()
    {
        gameControl = GameObject.FindGameObjectWithTag("oyunKontrolTag");
        colors = gameControl.GetComponent<GameControl>().colors;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        pointText.text = "Score: " + point;
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit"); // en yüksek puan bilgimi çekiyorum.

        ufoEnginePrefab.SetActive(true); //ufo motoru görünür yap

        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        changeUfosColor();
    }

    private void FixedUpdate()
    {
        ufoMovement();
        healthBarControl();
        energyBarControl();
    }

    private void ufoMovement()
    {
//#if UNITY_WEBGL
        horizontal = Input.GetAxisRaw("Horizontal"); //normal hareket
//#elif UNITY_ANDROID
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal"); //mobilde hareket
                                                                         //#endif
                                                                         /*hareket tuşuna basıldığında getaxisraw 0'dan 1 olur getaxis ise 0.1'den 0.2*/
        if (moveControl)
        {
            vec3 = new Vector3(horizontal * speed, rb2d.velocity.y, 0); // sırasıyla parantez içi: x ekseninde 10 hızında koş | y eksenindeki hızım neyse o olsun |
            rb2d.velocity = vec3;
        }
  
        rb2d.position = new Vector3( // ufonun ekranın dışına çıkmaması için sınır koordinatlarını belirliyoruz.
        Mathf.Clamp(rb2d.position.x, minX, maxX),
        transform.position.y
        );
    }

    private void changeUfosColor()
    {
#if UNITY_WEBGL
        if (Input.GetButtonDown("Up") || Input.GetButtonDown("Down"))
        {
            index += 1;
            if (index == colors.Length + 1)
            {
                index = 1;
            }
            spriteRenderer.color = colors[index - 1];
        }
#elif UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            index += 1;
            if (index == renkler.Length + 1)
            {
                index = 1;
            }
            spriteRenderer.color = renkler[index - 1];
        }
#endif
    }

    private void energyBarControl()
    {
        energyBar.fillAmount -= 0.00020f; //ufo ilerledikçe enerji barı azalıyor
        if (energyBar.fillAmount == 0) //enerji bari sifirlaninca ufo hareket edemesin.
        {
            moveControl = false;
        }

        if (energyBar.fillAmount >= 0.65f)
        {
            energyBar.color = energyBarColor[0];
        }
        else if (energyBar.fillAmount >= 0.33f)
        {
            energyBar.color = energyBarColor[1];
        }
        else
        {
            energyBar.color = energyBarColor[2];
        }
    }

    private void healthBarControl()
    {
        if (healthBar.fillAmount == 0) //can bari sifirlaninca oyun bitsin
        {
            oyunBitti();
        }

        if (healthBar.fillAmount >= 0.65f)
        {
            healthBar.color = healthBarColor[0];
        }
        else if (healthBar.fillAmount >= 0.33f)
        {
            healthBar.color = healthBarColor[1];
        }
        else
        {
            healthBar.color = healthBarColor[2];
        }
    }

    private void OnCollisionEnter2D(Collision2D col) // geçirgen olmayan bir yüzeye temas edildiğinde çalışır.
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
                point += 1;
                pointText.text = "Score: " + point;
                energyBar.fillAmount += 0.005f; //cisim toplanınca enerji artıyor
            }
            else
            {
                FindObjectOfType<SesKontrol>().sesOynat("Carpma"); //carpma sesini oynat
                Destroy(col.gameObject);
                healthBar.fillAmount -= 0.25f;
            }
        }

        if (col.transform.tag == "meteorTag")
        {
            Destroy(col.gameObject);
            oyunBitti();
        }


        if (col.transform.tag == "canVerenCisimTag")
        {
            if (healthBar.fillAmount < 1) //can full değilse
            {
                FindObjectOfType<SesKontrol>().sesOynat("CanveEnerji"); //can ve enerji sesini oynat
                Destroy(col.gameObject);
                healthBar.fillAmount += 0.5f;
            }
            else
            {
                col.transform.GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }

        if (col.transform.tag == "enerjiVerenCisimTag")
        {
            if (energyBar.fillAmount < 1) //enerji full değilse
            {
                FindObjectOfType<SesKontrol>().sesOynat("CanveEnerji"); //can ve enerji sesini oynat
                Destroy(col.gameObject);
                energyBar.fillAmount += 0.2f;
                moveControl = true;
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

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "randomEngelTag")
        {
            FindObjectOfType<SesKontrol>().sesOynat("EngelPuan"); //EngelPuan sesini oynat
            point += 5;
            pointText.text = "Score: " + point;
            energyBar.fillAmount += 0.015f; //engel aşılınca enerji artıyor
        }
    }

    private void anaMenuyeDon()
    {
        SceneManager.LoadScene(0);
    }

    private void yenidenBasla()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    private void oyunBitti()
    {
        ufoEnginePrefab.SetActive(false); //ufo motoru görünmez yap
        Instantiate(explosionPrefab, gameObject.transform.localPosition, Quaternion.identity); // patlama efekti oluştur
        FindObjectOfType<SesKontrol>().sesOynat("UfoPatlama"); //ufo patlama sesini oynat
        transform.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        point2Text.text = "Score: " + point;

        if (point > enYuksekPuan) // en yüksek puan için koşul
        {
            enYuksekPuan = point;
            PlayerPrefs.SetInt("enYuksekPuanKayit", enYuksekPuan); // en yüksek puanı kayıtlı tutuyoruz.
        }
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("enYuksekPuanKayit", enYuksekPuan); // en yuksek puanın gösterilmesi

        energyBar.fillAmount = 0; //enerji barını sıfırla
        healthBar.fillAmount = 0; //can barini sıfırla

        pauseGameButton.SetActive(false);

        /*Reklam göster*/
        gameOverTimer = PlayerPrefs.GetInt("oyunBittiSayac");
        gameOverTimer++;
        PlayerPrefs.SetInt("oyunBittiSayac", gameOverTimer);
        Debug.Log(gameOverTimer);

        if (PlayerPrefs.GetInt("oyunBittiSayac") == 3) //3 kere oyun bittiğinde reklam göster
        {
#if UNITY_ANDROID
            GameObject.FindGameObjectWithTag("reklamKontrolTag").GetComponent<ReklamKontrol>().reklamiGoster();
#endif
            PlayerPrefs.SetInt("oyunBittiSayac", 0);
        }
    }
}
