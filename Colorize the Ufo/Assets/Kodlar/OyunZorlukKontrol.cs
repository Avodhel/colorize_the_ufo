using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunZorlukKontrol : MonoBehaviour {

    float suredeBirArttirAzalt = 0f;
    bool hizArttirAzalt = true;

	void Start ()
    {
        Time.timeScale = 1;
	}
	
	void Update ()
    {
        oyunHiziniArttirAzalt();
    }

    void oyunHiziniArttirAzalt()
    {
        if (hizArttirAzalt)
        {
            suredeBirArttirAzalt += Time.deltaTime;

            while (suredeBirArttirAzalt > 8f) 
            {
                Time.timeScale += 0.02f; //hizlanma birimi
                //Debug.Log(Time.timeScale);
                suredeBirArttirAzalt = 0f;
                if (Time.timeScale > Random.Range(1.4f, 1.7f)) //hiz için üst sınır
                {
                    hizArttirAzalt = false;
                }
            }
        }
        else
        {
            suredeBirArttirAzalt += Time.deltaTime;

            while (suredeBirArttirAzalt > 5f)
            {
                Time.timeScale -= 0.06f; //azalma birimi
                //Debug.Log(Time.timeScale);
                suredeBirArttirAzalt = 0f;
                if (Time.timeScale < Random.Range(1f, 1.2f)) //hiz için alt sinir
                {
                    hizArttirAzalt = true;
                }
            }
        }
    }
}
