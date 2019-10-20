using UnityEngine;
using System;

public class SoundControl : MonoBehaviour {

    public SoundsList[] sesler; //seslerlistesi classımıza erişiyoruz.

	void Awake () //start metoduna benzer ancak oyun başladığından değil başlamadan önce çalışır
    {
        foreach (SoundsList s in sesler) //sesler listesindeki her bir ses(s) için
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
        SoundsList s =  Array.Find(sesler, ses => ses.sesAdi == sesAdi);
        if (s == null)
        {
            Debug.LogWarning(sesAdi + " adli ses dosyasi bulunamadi." );
            return;
        }
        s.source.Play();
    }
}
