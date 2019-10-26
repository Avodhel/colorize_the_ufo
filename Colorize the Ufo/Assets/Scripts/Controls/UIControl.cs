using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    [Header("Buttons")]
    public GameObject pauseGameButton;
    public Button soundMuteButton;
    public Button upgradeDurabilityButton;
    public Button upgradeSpeedButton;

    [Header("Sprites")]
    public Sprite gamePauseSprite;
    public Sprite gameContinueSprite;
    public Sprite soundUnmuteSprite;
    public Sprite soundMuteSprite;

    [Header("Images")]
    public Image changeColorImage;

    [Header("PC Control Tutorial")]
    public GameObject pcControlTutorial;
    public GameObject tutorialUpImage;
    public GameObject tutorialDownImage;
    public GameObject tutorialLeftImage;
    public GameObject tutorialRightImage;

    [Header("Panels")]
    public GameObject gamePausedPanel;
    public GameObject gameOverPanel;

    [Header("Texts")]
    public Text pointText;
    public Text speedText;
    public Text highScoreText;
    public Text spaceMineText;
    public Text ufoSpeedText;
    public Text ufoDurabilityText;
    public Text upgradeDurabilityButtonText;
    public Text upgradeSpeedButtonText;

    [Header("Colors")]
    public Color upgradeDurButtonColor;
    public Color upgradeSpeedButtonColor;

    public static UIControl UIManager { get; private set; }

    private void Awake()
    {
        if (UIManager == null)
        {
            UIManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        upgradeControl();

#if UNITY_WEBGL
        if (GameControl.gameManager.showPcControlTutorial)
        {
            pcControlTutorial.SetActive(true);
        }
#endif
    }

    public void upgradeControl()
    {
        //Durability
        if (GameControl.gameManager.spaceMineValue >= GameControl.gameManager.spaceMineForDurUpgrade)
        {
            upgradeDurabilityButton.GetComponent<Button>().interactable = true;
            upgradeDurabilityButton.GetComponent<Image>().color = upgradeDurButtonColor;
        }
        else
        {
            upgradeDurabilityButton.GetComponent<Button>().interactable = false;
            upgradeDurabilityButton.GetComponent<Image>().color = Color.gray;
        }
        upgradeDurabilityButtonText.text = "Upgrade (" + GameControl.gameManager.spaceMineForDurUpgrade + " Mines)";

        //Speed
        if (GameControl.gameManager.spaceMineValue >= GameControl.gameManager.spaceMineForSpeedUpgrade)
        {
            upgradeSpeedButton.GetComponent<Button>().interactable = true;
            upgradeSpeedButton.GetComponent<Image>().color = upgradeSpeedButtonColor;
        }
        else
        {
            upgradeSpeedButton.GetComponent<Button>().interactable = false;
            upgradeSpeedButton.GetComponent<Image>().color = Color.gray;
        }
        upgradeSpeedButtonText.text = "Upgrade (" + GameControl.gameManager.spaceMineForSpeedUpgrade + " Mines)";
    }
}
