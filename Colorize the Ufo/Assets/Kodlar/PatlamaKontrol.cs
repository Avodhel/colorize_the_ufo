using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatlamaKontrol : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public Sprite[] patlamaEfektler;
    int patlamaSayac = 0;
    float patlamaEfektZaman = 0f;
    //[HideInInspector]
    public bool patlamaKontrol = false;

    public Sprite[] ufoMotorEfektler;
    int motorSayac = 0;
    float motorAnimasyonZaman = 0;
    bool ileriGeriMotorKontrol = true;

    public GameObject ufo;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
        patlamaEfekti();
        ufoMotorEfekti();
	}

    void LateUpdate()
    {
        if (gameObject.transform.tag == "ufoMotorTag")
        {
            //alttaki kod satırı ile ufo motorunu, ufomuz ile birlikte hareket ettiriyoruz.
            gameObject.transform.position = new Vector3( ufo.transform.position.x,
                                                         gameObject.transform.position.y,
                                                         ufo.transform.position.z);
        }
    }

    void patlamaEfekti()
    {
        if (patlamaEfektler.Length != 0)
        {
            patlamaEfektZaman += Time.deltaTime;
            if (patlamaEfektZaman > 0.2f)
            {
                if (patlamaKontrol && patlamaSayac < patlamaEfektler.Length)
                {
                    spriteRenderer.sprite = patlamaEfektler[patlamaSayac];
                    gameObject.transform.localScale = new Vector3(2f, 2f, gameObject.transform.localScale.z);
                    patlamaSayac++;
                    patlamaEfektZaman = 0f;
                }
            }
        }
    }

    void ufoMotorEfekti()
    {
        if (ufoMotorEfektler.Length != 0)
        {
            motorAnimasyonZaman += Time.deltaTime;
            /*motor duman hızını düşürmek için koşul*/
            if (motorAnimasyonZaman > 0.2f) // her 0.2 saniyede bir bu koşula girecek.
            {
                motorAnimasyonZaman = 0;
                /*motor efekti için koşul*/
                if (ileriGeriMotorKontrol)
                {
                    spriteRenderer.sprite = ufoMotorEfektler[motorSayac]; // ufomotorefektler'in içindeki 3 motor texture'ını sırasıyla oynatıyoruz.
                    motorSayac++;
                    if (motorSayac == ufoMotorEfektler.Length) // motor sayacı dizinin sonuna ulaştığında
                    {
                        motorSayac--; // iki kere aynı texture'ın üst üste oynatılmaması için
                        ileriGeriMotorKontrol = false; // else'e girmesini sağlıyoruz.
                    }
                }
                else
                {
                    motorSayac--; //if'den gelen motorSayac 3 olacak ve hata verecek bu hatanın önüne geçiyoruz.
                    spriteRenderer.sprite = ufoMotorEfektler[motorSayac];
                    if (motorSayac == 0)
                    {
                        motorSayac++; // iki kere aynı texture'ın üst üste oynatılmaması için
                        ileriGeriMotorKontrol = true; //tekrar if'e girmesini sağlıyoruz.
                    }
                }
            }

        }
    }
}
