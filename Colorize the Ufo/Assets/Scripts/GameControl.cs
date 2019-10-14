using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [Header("Obstacle")]
    [SerializeField]
    [Range(1, 25)]
    private int obstacleRate = 10;
    [SerializeField]
    [Range(0.5f, 3f)]
    private float obstacleSpeed = 2;
    [SerializeField]
    [Range(1f, 20f)]
    private float obstacleSpawnFrequency = 6;
    [SerializeField]
    private GameObject obstaclePrefab;
    [SerializeField]
    private GameObject[] obstacleTypes;

    [Header("Colors")]
    public Color[] colors;
    public Color[] healthBarColors;
    public Color[] energyBarColors;

    [Header("UI Elements")]
    [SerializeField]
    private GameObject pauseGameButton;
    [SerializeField]
    private Button soundMuteButton;
    [SerializeField]
    private Sprite gamePauseSprite;
    [SerializeField]
    private Sprite gameContinueSprite;
    [SerializeField]
    private Sprite soundUnmuteSprite;
    [SerializeField]
    private Sprite soundMuteSprite;
    [SerializeField]
    private Image changeColorImage;
    [SerializeField]
    private GameObject gamePausedPanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text pointText;
    [SerializeField]
    private Text highScoreText;

    private UfoControl ufoControl;
    private GameObject[] obstacles;
    private GameObject obstacleChilds;

    private float gamePausedTimeScale;
    private float changeObstacleTime = 0f;
    private int timer = 0;
    private int firstPlay;
    private static int point { get; set; }
    private static int enYuksekPuan { get; set; }
    private int gameOverCounter = 0;

    public static GameControl gameManager { get; private set; } //basic singleton

    private void Awake()
    {
        gameManager = this;

        resetScore();
        loadHighscore();
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        isThisFirstPlay();
        soundControl();
        spawnObstacles();

        ufoControl = GameObject.FindGameObjectWithTag("ufoTag").GetComponent<UfoControl>();
        obstacleChilds = GameObject.Find(obstaclePrefab.transform.name + "(Clone)"); //engel objesini bul
    }

    private void Update()
    {
        obstaclePosAssign();
    }

    private void spawnObstacles()
    {
        obstacles = new GameObject[obstacleRate]; //kacAdetEngel'e verilen sayı kadar engel objesi
        for (int i = 0; i < obstacles.Length; i++) // engeller dizisi kadar obje oluştur
        {
            obstaclePrefab = obstacleTypes[Random.Range(0, obstacleTypes.Length)]; //birden farklı engel tipi arasından seçim
            obstacles[i] = Instantiate(obstaclePrefab, new Vector2(-10, -10), Quaternion.identity); // engel objelerinin oluşturulması
            Rigidbody2D rb2D = obstacles[i].AddComponent<Rigidbody2D>(); //engelimize koddan rigidbody ekledik.
            rb2D.bodyType = RigidbodyType2D.Kinematic; //!!!dynamic olunca engeller savruluyor
            rb2D.gravityScale = 0; //oluşan engellerin düşmemesi için 
            rb2D.velocity = new Vector2(0, -obstacleSpeed); // engellerimizin hareket etmesi için.
        }
    }

    private void obstaclePosAssign() // oluşturulan engellerin random pozisyonlarda oluşması için metot
    {
        changeObstacleTime += Time.deltaTime;
        if (changeObstacleTime > obstacleSpawnFrequency) // belirlenen saniyede bir bu koşula girecek ve engelleri oluşturacak.
        {
            changeObstacleTime = 0;
            float xEkseniDegisim = Random.Range(-10f, -2f); // x eksenini random olarak değiştirerek engellerin random bir konumda gelmesini sağlıyoruz.
            obstacles[timer].transform.position = new Vector3(xEkseniDegisim, 7f); // engellerimizin oluşum alanını belirliyoruz.
                                                                                   /*!!! Vector3 iki tane değer de alabilir.*/
            timer++;
            if (timer >= obstacles.Length) // dizideki obje sayısına ulaştığımda
            {
                timer = 0; // sayacı sıfırla
            }

            if (obstacles[timer].transform.position.y <= -6f) // engel sahneden çıktığında rengini ve triggerını değiştirmek için koşul
            {
                for (int a = 0; a < obstacleChilds.transform.childCount; a++)
                {
                    if (obstacles[timer].transform.GetChild(a).tag == "randomEngelTag")
                    {
                        obstacles[timer].transform.GetChild(a).GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)]; //renk degistir

                        if (obstacles[timer].transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger == true) //istriggeri açıksa 
                        {
                            obstacles[timer].transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger = false; //kapat
                        }
                    }
                }
            }
        }
    }

    private void resetScore()
    {
        point = 0;
        pointText.text = "Score: " + point;
    }

    public void increaseScore(int value)
    {
        point += value;
        pointText.text = "Score: " + point;
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true);
        assignHighscore();
        pauseGameButton.SetActive(false);

        //game ver counter for ad
        gameOverCounter = PlayerPrefs.GetInt("oyunBittiSayac");
        gameOverCounter++;
        PlayerPrefs.SetInt("oyunBittiSayac", gameOverCounter);
        Debug.Log(gameOverCounter);

        showAd();
    }

    private void loadHighscore()
    {
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit"); // en yüksek puan bilgimi çekiyorum.
        //Debug.Log(enYuksekPuan);
    }

    private void assignHighscore()
    {
        if (point > enYuksekPuan) // en yüksek puan için koşul
        {
            enYuksekPuan = point;
            PlayerPrefs.SetInt("enYuksekPuanKayit", enYuksekPuan); // en yüksek puanı kayıtlı tutuyoruz.
        }
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("enYuksekPuanKayit", enYuksekPuan); // en yuksek puanın gösterilmesi
    }

    public void yenidenBasla()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void oyunuDurdurveDevamEt() //oyunu durdur butonuna basıldığında
    {
        if (Time.timeScale >= 1) //oyun devam ediyorsa
        {
            gamePausedTimeScale = Time.timeScale; //kalınan hız bilgisini al
            Time.timeScale = 0; // oyunu durdur
            pauseGameButton.GetComponent<Image>().sprite = gameContinueSprite;
            gamePausedPanel.SetActive(true);
            AudioListener.pause = true; //sesleri kapat
            changeColorImage.enabled = false; // oyun durduğunda ufonun rengi değiştirilemesin
        }
        else if (Time.timeScale == 0) // oyun durmuşsa
        {
            Time.timeScale = gamePausedTimeScale; //oyuna kalınan hızdan devam et
            pauseGameButton.GetComponent<Image>().sprite = gamePauseSprite;
            gamePausedPanel.SetActive(false);
            AudioListener.pause = false;//sesi tekrar ac
            changeColorImage.enabled = true; // ufo rengi değiştirmeyi tekrar aktif et
        }
    }

    public void sesleriKapatveAc()
    {
        if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 0) //ses açıksa
        {
            soundMuteButton.GetComponent<Image>().sprite = soundMuteSprite;
            AudioListener.volume = 0f; //sesi kapat
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 1);
        }
        else if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 1) //ses kapalıysa
        {
            soundMuteButton.GetComponent<Image>().sprite = soundUnmuteSprite;
            AudioListener.volume = 1f; //sesi ac
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 0);
        }
    }

    private void soundControl() //restart sonrası ses acik veya kapali sprite sorununu çözen fonksiyon
    {
        if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 0)
        {
            soundMuteButton.GetComponent<Image>().sprite = soundUnmuteSprite;
            AudioListener.volume = 1f; //sesi ac
        }
        else if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 1)
        {
            soundMuteButton.GetComponent<Image>().sprite = soundMuteSprite;
            AudioListener.volume = 0f; //sesi kapat
        }
    }

    private void isThisFirstPlay() //oyun ilk kez oynanıyorsa tutorial gözükmesi için fonksiyon
    {
        firstPlay = PlayerPrefs.GetInt("ilkKezMiOynaniyor");

        if (firstPlay <= 2)
        {
            firstPlay = firstPlay + 1;
            PlayerPrefs.SetInt("ilkKezMiOynaniyor", firstPlay);
        }
    }

    private void showAd()
    {
        if (PlayerPrefs.GetInt("oyunBittiSayac") == 3) //3 kere oyun bittiğinde reklam göster
        {
#if UNITY_ANDROID
            GameObject.FindGameObjectWithTag("reklamKontrolTag").GetComponent<AdControl>().reklamiGoster();
#endif
            PlayerPrefs.SetInt("oyunBittiSayac", 0);
        }
    }
}
