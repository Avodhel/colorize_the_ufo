using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    [Header("Buttons")]
    public GameObject pauseGameButton;
    public Button soundMuteButton;

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
}
