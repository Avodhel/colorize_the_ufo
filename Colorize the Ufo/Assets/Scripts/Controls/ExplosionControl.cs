using UnityEngine;

public class ExplosionControl : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    public Animator anim;

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
