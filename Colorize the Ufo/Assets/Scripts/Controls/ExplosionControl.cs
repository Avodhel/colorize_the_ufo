using UnityEngine;

public class ExplosionControl : MonoBehaviour {

    [SerializeField]
    [Header("Explosion")]
    private Sprite[] explosionEffectSprites;

    private SpriteRenderer spriteRenderer;

    private int explosionTimer = 0;
    private float explosionEffectTimer = 0f;

    private void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	private void Update ()
    {
        explosionEffect();
	}

    private void explosionEffect()
    {
        if (explosionEffectSprites.Length != 0)
        {
            explosionEffectTimer += Time.deltaTime;
            if (explosionEffectTimer > 0.2f)
            {
                if (explosionTimer < explosionEffectSprites.Length)
                {
                    spriteRenderer.sprite = explosionEffectSprites[explosionTimer];
                    gameObject.transform.localScale = new Vector3(2f, 2f, gameObject.transform.localScale.z);
                    explosionTimer++;
                    explosionEffectTimer = 0f;
                }
            }
        }
    }
}
