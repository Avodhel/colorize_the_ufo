using UnityEngine;
using System;

public class SesKontrol : MonoBehaviour {

    public SeslerListesi[] sesler; //seslerlistesi classımıza erişiyoruz.

	void Awake () //start metoduna benzer ancak oyun başladığından değil başlamadan önce çalışır
    {
        foreach (SeslerListesi s in sesler) //sesler listesindeki her bir ses(s) için
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    public void sesOynat(string sesAdi) //bu metot ile ismine göre seslerimizi istediğimiz yerde oynatacağız.
    {
        SeslerListesi s =  Array.Find(sesler, ses => ses.sesAdi == sesAdi);
        if (s == null)
        {
            Debug.LogWarning(sesAdi + " adli ses dosyasi bulunamadi." );
            return;
        }
        s.source.Play();
    }
}
