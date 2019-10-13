using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobilKontrol : MonoBehaviour {
#if UNITY_ANDROIDk
    public Image leftImage;
    public Text leftText;
    public Image rightImage;
    public Text rightText;
    public Image changeColorImage;
    public Text changeColorText;

	void Start ()
    {
        if (PlayerPrefs.GetInt("ilkKezMiOynaniyor") == 1) //eğer oyun ilk kez oynanıyorsa
        {
            leftImage.color = new Color(leftImage.color.r, leftImage.color.g, leftImage.color.b, 1f);
            rightImage.color = new Color(rightImage.color.r, rightImage.color.g, rightImage.color.b, 1f);
            changeColorImage.color = new Color(changeColorImage.color.r, changeColorImage.color.g, changeColorImage.color.b, 1f);

            leftText.enabled = true;
            rightText.enabled = true;
            changeColorText.enabled = true;
        }
    }
	
    public void OnMouseDown()
    {
        if (gameObject.name == "Left")
        {
            leftImage.color = new Color(leftImage.color.r, leftImage.color.g, leftImage.color.b, 0f);
            leftText.enabled = false;
        }

        if (gameObject.name == "Right")
        {
            rightImage.color = new Color(rightImage.color.r, rightImage.color.g, rightImage.color.b, 0f);
            rightText.enabled = false;
        }

        if (gameObject.name == "ChangeColor")
        {
            changeColorImage.color = new Color(changeColorImage.color.r, changeColorImage.color.g, changeColorImage.color.b, 0f);
            changeColorText.enabled = false;
        }
    }
#endif
}
