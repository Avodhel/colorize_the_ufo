using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UcanCisimHareket : MonoBehaviour {

    Rigidbody2D fizik;
    public float cisimHizi;
    public float kinematicYapmaSiniri;
    public Color[] cisimRenkler;
    SpriteRenderer spriteRenderer;
    public Sprite[] meteorTipleri;

    void Start ()
    {
        fizik = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        cisimHareket();
        cisimRenkDegistir();
        meteorRandomTip();
    }

    void Update ()
    {
        cisimDondur();
        cismiKinematicYap();
    }

    void cisimHareket()
    {
        fizik.velocity = new Vector2(0, -cisimHizi);
    }

    void cisimDondur()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)) * 8f);
        if (transform.tag != "meteorTag")
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)) * Time.deltaTime);
        }
    }

    void cisimRenkDegistir()
    {
        if (cisimRenkler.Length != 0)
        {
            spriteRenderer.color = cisimRenkler[Random.Range(0, cisimRenkler.Length)];
        }
    }

    void meteorRandomTip()
    {
        if (gameObject.transform.tag == "meteorTag")
        {
            spriteRenderer.sprite = meteorTipleri[Random.Range(0, meteorTipleri.Length)];
        }
    }

    void cismiKinematicYap() //iki kinematic obje arasında collision çalışmadığı için böyle bir çözüm buldum.
    {
        if (transform.position.y <= kinematicYapmaSiniri)
        {
            fizik.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "engellerTag"         || 
            col.transform.tag == "meteorTag"           || 
            col.transform.tag == "ucanCisimTag"        ||
            col.transform.tag == "canVerenCisimTag"    ||
            col.transform.tag == "enerjiVerenCisimTag" ||
            col.transform.tag == "yavaslatanCisimTag")
        {
            if (transform.gameObject.tag == "ucanCisimTag")
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
