using UnityEngine;

public class UfoEngineControl : MonoBehaviour {

    [SerializeField]
    [Header("Ufo Engine")]
    private Sprite[] engineEffectSprites;

    [SerializeField]
    [Header("Ufo")]
    private GameObject ufo;

    private SpriteRenderer spriteRenderer;

    private int engineTimer = 0;
    private float engineAnimTimer = 0;
    private bool engineAnimControl = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ufoEngineEffect();
    }

    private void LateUpdate()
    {
        if (gameObject.transform.tag == "ufoMotorTag")
        {
            //alttaki kod satırı ile ufo motorunu, ufomuz ile birlikte hareket ettiriyoruz.
            gameObject.transform.position = new Vector3(ufo.transform.position.x,
                                                         gameObject.transform.position.y,
                                                         ufo.transform.position.z);
        }
    }

    private void ufoEngineEffect()
    {
        if (engineEffectSprites.Length != 0)
        {
            engineAnimTimer += Time.deltaTime;
            /*motor duman hızını düşürmek için koşul*/
            if (engineAnimTimer > 0.2f) // her 0.2 saniyede bir bu koşula girecek.
            {
                engineAnimTimer = 0;
                /*motor efekti için koşul*/
                if (engineAnimControl)
                {
                    spriteRenderer.sprite = engineEffectSprites[engineTimer]; // ufomotorefektler'in içindeki 3 motor texture'ını sırasıyla oynatıyoruz.
                    engineTimer++;
                    if (engineTimer == engineEffectSprites.Length) // motor sayacı dizinin sonuna ulaştığında
                    {
                        engineTimer--; // iki kere aynı texture'ın üst üste oynatılmaması için
                        engineAnimControl = false; // else'e girmesini sağlıyoruz.
                    }
                }
                else
                {
                    engineTimer--; //if'den gelen motorSayac 3 olacak ve hata verecek bu hatanın önüne geçiyoruz.
                    spriteRenderer.sprite = engineEffectSprites[engineTimer];
                    if (engineTimer == 0)
                    {
                        engineTimer++; // iki kere aynı texture'ın üst üste oynatılmaması için
                        engineAnimControl = true; //tekrar if'e girmesini sağlıyoruz.
                    }
                }
            }

        }
    }
}
