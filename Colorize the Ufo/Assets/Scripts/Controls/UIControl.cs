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
        upgradeSystem();
    }

    public void upgradeSystem()
    {
        //Durability
        if (GameControl.gameManager.spaceMineValue < GameControl.gameManager.spaceMineForDurUpgrade)
        {
            upgradeDurabilityButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            upgradeDurabilityButton.GetComponent<Button>().interactable = true;
        }
        upgradeDurabilityButtonText.text = "Upgrade (" + GameControl.gameManager.spaceMineForDurUpgrade + " Mines)";

        //Speed
        if (GameControl.gameManager.spaceMineValue < GameControl.gameManager.spaceMineForSpeedUpgrade)
        {
            upgradeSpeedButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            upgradeSpeedButton.GetComponent<Button>().interactable = true;
        }
        upgradeSpeedButtonText.text = "Upgrade (" + GameControl.gameManager.spaceMineForSpeedUpgrade + " Mines)";
    }
}
