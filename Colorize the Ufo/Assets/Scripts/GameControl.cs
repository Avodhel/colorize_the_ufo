using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
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

    private float gamePausedTimeScale;
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

        showAd();
    }

    private void loadHighscore()
    {
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit"); // en yüksek puan bilgimi çekiyorum.
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
