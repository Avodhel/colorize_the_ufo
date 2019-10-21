using UnityEngine;

public class ExplosionControl : MonoBehaviour {

    [Header("Animator")]
    public Animator anim;

    private SpriteRenderer spriteRenderer;

    private void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void makeVisible()
    {
        spriteRenderer.enabled = true;
    }

    private void makeInvisible()
    {
        spriteRenderer.enabled = false;
    }
}
