using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UcanCisimlerKontrol : MonoBehaviour {

    public GameObject[] ucanCisimler;
    GameObject ucanCisim;
    public float baslangicBeklemeSuresi;
    public int minCisimSayisi;
    public int maxCisimSayisi;
    public float ucanCisimOlusturmaSuresi;
    public float ucanCisimGrupOlusturmaSuresi;
    public Vector2 randomPozisyon;

    public GameObject[] nadirCisimler;
    GameObject nadirCisim;
    public int minNadirCisimSayisi;
    public int maxNadirCisimSayisi;
    public float nadirCisimOlusturmaSuresi;
    public float nadirCisimGrupOlusturmaSuresi;

    void Start ()
    {
        StartCoroutine(randomCisimOlustur());
        StartCoroutine(nadirCisimOlustur());
	}
	
	void Update ()
    {
		
	}

    IEnumerator randomCisimOlustur()
    {
        yield return new WaitForSeconds(baslangicBeklemeSuresi);

        while (true) // sonsuz bir döngü 
        {
            for (int i = 0; i < Random.Range(minCisimSayisi, maxCisimSayisi); i++)
            {
                ucanCisim = ucanCisimler[Random.Range(0, ucanCisimler.Length)];
                Vector2 vec = new Vector2(Random.Range(-randomPozisyon.x, randomPozisyon.x), randomPozisyon.y);
                if (ucanCisim.tag == "meteorTag")
                {
                    Instantiate(ucanCisim, vec, Quaternion.identity);
                }
                else
                {
                    Instantiate(ucanCisim, vec, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                }
                yield return new WaitForSeconds(ucanCisimOlusturmaSuresi);
            }
            yield return new WaitForSeconds(ucanCisimGrupOlusturmaSuresi);
        }
    }

    IEnumerator nadirCisimOlustur()
    {
        yield return new WaitForSeconds(20); //başlangıç bekleme suresi

        while (true)
        {
            for (int i = 0; i < Random.Range(minNadirCisimSayisi, maxNadirCisimSayisi); i++)
            {
                nadirCisim = nadirCisimler[Random.Range(0, nadirCisimler.Length)];
                Vector2 vec = new Vector2(Random.Range(-randomPozisyon.x, randomPozisyon.x), randomPozisyon.y);
                    Instantiate(nadirCisim, vec, Quaternion.identity);
                yield return new WaitForSeconds(nadirCisimOlusturmaSuresi);
            }
            yield return new WaitForSeconds(nadirCisimGrupOlusturmaSuresi);
        }
    }
}
