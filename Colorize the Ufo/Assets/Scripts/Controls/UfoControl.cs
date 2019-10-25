using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput; //library for mobile entegration

public class UfoControl : MonoBehaviour
{
    [Header("Ufo Speed")]
    [SerializeField]
    private float ufoSpeed = 3f;

    [Header("Ufo Durability")]
    [SerializeField]
    private int ufoDurability = 100;
    [SerializeField]
    private float durEffectValue = 0.25f;

    [Header("Movement Border")]
    [SerializeField]
    private float maxX = 3.1f;
    [SerializeField]
    private float minX = -3.1f;

    [Header("Bars")]
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
    private Color[] colors;
    private Color[] healthBarColors;
    private Color[] energyBarColors;

    private float horizontal = 0f;
    private int index = 1;
    private bool moveControl = true;

    private void Awake()
    {
        colors = GameControl.gameManager.colors;
        healthBarColors = GameControl.gameManager.healthBarColors;
        energyBarColors = GameControl.gameManager.energyBarColors;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ufoEnginePrefab.SetActive(true); //ufo motoru görünür yap
    }

    private void Update()
    {
        changeUfosColor();
    }

    private void FixedUpdate()
    {
        ufoMovement();
        energyBarAction("reduce", 0.00020f);
    }

    #region User Input
    private void userInput()
    {
#if UNITY_WEBGL
        horizontal = Input.GetAxisRaw("Horizontal"); //normal hareket
#elif UNITY_ANDROID
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal"); //mobilde hareket
                                                                         //#endif
                                                                         /*hareket tuşuna basıldığında getaxisraw 0'dan 1 olur getaxis ise 0.1'den 0.2*/
#endif
    }
    #endregion

    #region Ufo Movement
    private void ufoMovement()
    {
        userInput();

        if (moveControl)
        {
            vec3 = new Vector3(horizontal * ufoSpeed, rb2d.velocity.y, 0);
            rb2d.velocity = vec3;
        }

        rb2d.position = new Vector3( // ufonun ekranın dışına çıkmaması için sınır koordinatlarını belirliyoruz.
        Mathf.Clamp(rb2d.position.x, minX, maxX),
        transform.position.y
        );
    }
    #endregion

    #region Ufo's Color
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
            if (index == colors.Length + 1)
            {
                index = 1;
            }
            spriteRenderer.color = colors[index - 1];
        }
#endif
    }
    #endregion

    #region Ufo explode
    public void ufoExploded()
    {
        ufoEnginePrefab.SetActive(false); //ufo motoru görünmez yap
        Instantiate(explosionPrefab, gameObject.transform.localPosition, Quaternion.identity); // patlama efekti oluştur
        FindObjectOfType<SoundControl>().sesOynat("UfoPatlama"); //ufo patlama sesini oynat
        transform.gameObject.SetActive(false);

        energyBarAction("reset", 0);
        healthBarAction("reset", 0);

        GameControl.gameManager.gameOver();
    }
    #endregion

    #region Health and Energy Bars
    public void healthBarAction(string con, float value)
    {
        if (con == "increase")
        {
            healthBar.fillAmount += value;
        }
        else if (con == "reduce")
        {
            healthBar.fillAmount -= value;
            if (healthBar.fillAmount <= 0) //can bari sifirlaninca ufo patlasın
            {
                ufoExploded();
            }
        }
        barColorize(healthBar, healthBarColors);

        if (con == "reset")
        {
            healthBar.fillAmount = 0;
        }
    }

    private void energyBarAction(string con, float value)
    {
        if (con == "increase")
        {
            energyBar.fillAmount += value;
        }
        else if (con == "reduce")
        {
            energyBar.fillAmount -= value; //ufo ilerledikçe enerji barı azalıyor
            if (energyBar.fillAmount == 0) //enerji bari sifirlaninca ufo hareket edemesin.
            {
                moveControl = false;
            }
        }
        barColorize(energyBar, energyBarColors);

        if (con == "reset")
        {
            energyBar.fillAmount = 0;
        }
    }

    private void barColorize(Image bar, Color[] colors)
    {
        //colorize bars according to amount
        if (bar.fillAmount >= 0.65f)
        {
            bar.color = colors[0];
        }
        else if (bar.fillAmount >= 0.33f)
        {
            bar.color = colors[1];
        }
        else
        {
            bar.color = colors[2];
        }
    }
    #endregion

    #region Ufo Upgrade System
    public void ufoSpeedUpgrade()
    {
        ufoSpeed += 0.1f;

        UIControl.UIManager.ufoSpeedText.text = "Ufo Movement Speed: " + System.Math.Round(ufoSpeed, 2);
    }

    public void ufoDurUpgrade()
    {
        ufoDurability += 25;
        durEffectValue = 1 / (ufoDurability / 25);

        UIControl.UIManager.ufoDurabilityText.text = "Ufo Durability: " + ufoDurability;
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D col) // geçirgen olmayan bir yüzeye temas edildiğinde çalışır.
    {
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
            ufoExploded();
        }

        if (col.transform.tag == "ucanCisimTag")
        {
            if (col.transform.GetComponent<SpriteRenderer>().color == transform.GetComponent<SpriteRenderer>().color)
            {
                FindObjectOfType<SoundControl>().sesOynat("Puan"); //puan sesini oynat
                col.gameObject.SetActive(false);
                GameControl.gameManager.increaseScore(1);
                energyBarAction("increase", 0.005f);
                GameControl.gameManager.spaceMine("increase", 1);
            }
            else
            {
                FindObjectOfType<SoundControl>().sesOynat("Carpma"); //carpma sesini oynat
                col.gameObject.SetActive(false);
                healthBarAction("reduce", durEffectValue);
            }
        }

        if (col.transform.tag == "meteorTag")
        {
            col.gameObject.SetActive(false);
            ufoExploded();
        }


        if (col.transform.tag == "canVerenCisimTag")
        {
            if (healthBar.fillAmount < 1) //can full değilse
            {
                FindObjectOfType<SoundControl>().sesOynat("CanveEnerji"); //can ve enerji sesini oynat
                col.gameObject.SetActive(false);
                healthBarAction("increase", durEffectValue * 2);
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
                FindObjectOfType<SoundControl>().sesOynat("CanveEnerji"); //can ve enerji sesini oynat
                col.gameObject.SetActive(false);
                energyBarAction("increase", 0.2f);
                moveControl = true;
            }
            else
            {
                col.transform.GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }

        if (col.transform.tag == "yavaslatanCisimTag")
        {
            if (Time.timeScale > 0.1f) //hiz 0.1'den büyükse
            {
                FindObjectOfType<SoundControl>().sesOynat("Yavaslatma"); //yavaslatma sesini oynat
                col.gameObject.SetActive(false);
                //if (Time.timeScale > 0.1f)
                //{
                GameControl.gameManager.gameSpeed("reduce", 0.1f);
                //}
            }
            else
            {
                col.transform.GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }
    }
    #endregion

    #region Trigger
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "randomEngelTag")
        {
            FindObjectOfType<SoundControl>().sesOynat("EngelPuan"); //EngelPuan sesini oynat
            GameControl.gameManager.increaseScore(5);
            energyBarAction("increase", 0.015f);
        }
    }
    #endregion
}
