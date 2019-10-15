using UnityEngine;

public class SpaceObject : MonoBehaviour, ISpaceObject
{
    [Header("Object")]
    public float objectSpeed = 2f;
    public float makeKinematicBorder = -4.3f;

    [HideInInspector]
    public Rigidbody2D rb2d;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        objectMovement();
    }

    public void Update()
    {
        makeObjectKinematic();
    }

    public void objectMovement()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -objectSpeed);
    }

    public void makeObjectKinematic() //iki kinematic obje arasında collision çalışmadığı için böyle bir çözüm buldum.
    {
        if (transform.position.y <= makeKinematicBorder)
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
