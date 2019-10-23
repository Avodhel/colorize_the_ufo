using UnityEngine;
using System;
using UnityEngine.UI;

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

    private void Start()
    {
        soundControl();
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

    public void soundsMuteAndUnmute()
    {
        if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 0) //ses açıksa
        {
            UIControl.UIManager.soundMuteButton.GetComponent<Image>().sprite = UIControl.UIManager.soundMuteSprite;
            AudioListener.volume = 0f; //sesi kapat
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 1);
        }
        else if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 1) //ses kapalıysa
        {
            UIControl.UIManager.soundMuteButton.GetComponent<Image>().sprite = UIControl.UIManager.soundUnmuteSprite;
            AudioListener.volume = 1f; //sesi ac
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 0);
        }
    }

    private void soundControl() //restart sonrası ses acik veya kapali sprite sorununu çözen fonksiyon
    {
        if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 0)
        {
            UIControl.UIManager.soundMuteButton.GetComponent<Image>().sprite = UIControl.UIManager.soundUnmuteSprite;
            AudioListener.volume = 1f; //sesi ac
        }
        else if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 1)
        {
            UIControl.UIManager.soundMuteButton.GetComponent<Image>().sprite = UIControl.UIManager.soundMuteSprite;
            AudioListener.volume = 0f; //sesi kapat
        }
    }
}
