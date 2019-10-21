using UnityEngine;

public class UfoEngineControl : MonoBehaviour {

    [SerializeField]
    [Header("Ufo")]
    private GameObject ufo;
    [Header("Animator")]
    public Animator anim;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        moveEngineWithUfo();
    }

    private void moveEngineWithUfo()
    {
        //alttaki kod satırı ile ufo motorunu, ufomuz ile birlikte hareket ettiriyoruz.
        gameObject.transform.position = new Vector3(ufo.transform.position.x,
                                                        gameObject.transform.position.y,
                                                        ufo.transform.position.z);
    }
}
