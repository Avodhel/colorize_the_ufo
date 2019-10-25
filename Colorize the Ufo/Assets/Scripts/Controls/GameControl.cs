using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [Header("Colors")]
    public Color[] colors;
    public Color[] healthBarColors;
    public Color[] energyBarColors;

    private int gameOverCounter = 0;
    private float gamePausedTimeScale { get; set; }
    private static int point { get; set; }
    private static int enYuksekPuan { get; set; }
    public int spaceMineValue { get; set; }
    public int spaceMineForDurUpgrade = 5;
    public int spaceMineForSpeedUpgrade = 10;

    public static GameControl gameManager { get; private set; } //basic singleton

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        loadGameData();
        loadValues();
        loadHighscore();
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //SaveSystem.deleteDatas();
    }

    private void loadValues()
    {
        point = 0;
        UIControl.UIManager.pointText.text = "Score: " + point;

        Time.timeScale = 1f;
        UIControl.UIManager.speedText.text = "Speed: " + Time.timeScale;

        UIControl.UIManager.spaceMineText.text = "Space Mine: " + spaceMineValue;
    }

    #region Score and Highscore
    public void increaseScore(int value)
    {
        point += value;
        UIControl.UIManager.pointText.text = "Score: " + point;
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
        UIControl.UIManager.highScoreText.text = "High Score: " + PlayerPrefs.GetInt("enYuksekPuanKayit", enYuksekPuan); // en yuksek puanın gösterilmesi
    }
    #endregion

    public void gameSpeed(string state, float value)
    {
        if (state == "increase")
        {
            Time.timeScale += value;
        }
        else if(state == "reduce")
        {
            Time.timeScale -= value;
        }

        UIControl.UIManager.speedText.text = "Speed: " + System.Math.Round(Time.timeScale, 2);
    }

    public void spaceMine(string state, int value)
    {
        if (state == "increase")
        {
            spaceMineValue += value;
        }
        else if (state == "reduce")
        {
            spaceMineValue -= value;
        }

        UIControl.UIManager.spaceMineText.text = "Space Mine: " + spaceMineValue;
    }

    public void gameOver()
    {
        UIControl.UIManager.gameOverPanel.SetActive(true);
        assignHighscore();
        UIControl.UIManager.pauseGameButton.SetActive(false);

        //check out mines for upgrade system
        UIControl.UIManager.upgradeControl();

        //save game datas
        saveGameData();

        //game over counter for ad
        gameOverCounter = PlayerPrefs.GetInt("oyunBittiSayac");
        gameOverCounter++;
        PlayerPrefs.SetInt("oyunBittiSayac", gameOverCounter);

        showAd();
    }

    #region Show Ad
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
    #endregion

    #region Button Actions
    public void goToMainMenu()
    {
        SceneControl.sceneManager.loadScene(0);
    }

    public void restartGame()
    {
        UIControl.UIManager.gameOverPanel.SetActive(false);
        SceneControl.sceneManager.loadScene(1);
    }

    public void gamePauseAndUnpause() //oyunu durdur butonuna basıldığında
    {
        if (Time.timeScale > 0) //oyun devam ediyorsa
        {
            gamePausedTimeScale = Time.timeScale; //kalınan hız bilgisini al
            Time.timeScale = 0; // oyunu durdur
            UIControl.UIManager.pauseGameButton.GetComponent<Image>().sprite = UIControl.UIManager.gameContinueSprite;
            UIControl.UIManager.gamePausedPanel.SetActive(true);
            AudioListener.pause = true; //sesleri kapat
            UIControl.UIManager.changeColorImage.enabled = false; // oyun durduğunda ufonun rengi değiştirilemesin
        }
        else if (Time.timeScale == 0) // oyun durmuşsa
        {
            Time.timeScale = gamePausedTimeScale; //oyuna kalınan hızdan devam et
            UIControl.UIManager.pauseGameButton.GetComponent<Image>().sprite = UIControl.UIManager.gamePauseSprite;
            UIControl.UIManager.gamePausedPanel.SetActive(false);
            AudioListener.pause = false;//sesi tekrar ac
            UIControl.UIManager.changeColorImage.enabled = true; // ufo rengi değiştirmeyi tekrar aktif et
        }
    }
    #endregion

    #region Save and Load System
    public void saveGameData()
    {
        SaveSystem.saveGameData(this);
    }

    public void loadGameData()
    {
        GameData gameData = SaveSystem.loadGameData();

        spaceMineValue = gameData.spaceMineValue;
        spaceMineForDurUpgrade = gameData.spaceMineForDurUpgrade;
        spaceMineForSpeedUpgrade = gameData.spaceMineForSpeedUpgrade;
    }
    #endregion
}
