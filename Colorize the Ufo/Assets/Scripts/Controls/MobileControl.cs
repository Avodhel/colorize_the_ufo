using UnityEngine;
using UnityEngine.UI;

public class MobileControl : MonoBehaviour {
#if UNITY_ANDROID

    private Image leftImage;
    private Text leftText;
    private Image rightImage;
    private Text rightText;
    private Image changeColorImage;
    private Text changeColorText;

    public static MobileControl mobileControlManager { get; private set; }

    private void Awake()
    {
        mobileControlManager = this;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Left")
            {
                Transform left = transform.GetChild(i);
                leftImage = left.GetComponent<Image>();
                leftText = left.GetChild(0).GetComponent<Text>();
            }
            if (transform.GetChild(i).name == "Right")
            {
                Transform right = transform.GetChild(i);
                rightImage = right.GetComponent<Image>();
                rightText = right.GetChild(0).GetComponent<Text>();
            }
            if (transform.GetChild(i).name == "ChangeColor")
            {
                Transform changeColor = transform.GetChild(i);
                changeColorImage = changeColor.GetComponent<Image>();
                changeColorText = changeColor.GetChild(0).GetComponent<Text>();
            }
        }
    }

    private void Start ()
    {
        MakeVisible();
    }

    private void MakeVisible()
    {
        leftImage.color = new Color(leftImage.color.r, leftImage.color.g, leftImage.color.b, 1f);
        rightImage.color = new Color(rightImage.color.r, rightImage.color.g, rightImage.color.b, 1f);
        changeColorImage.color = new Color(changeColorImage.color.r, changeColorImage.color.g, changeColorImage.color.b, 1f);

        leftText.enabled = true;
        rightText.enabled = true;
        changeColorText.enabled = true;
    }

    public void LeftClicked()
    {
        leftImage.color = new Color(leftImage.color.r, leftImage.color.g, leftImage.color.b, 0f);
        leftText.enabled = false;
    }

    public void RightClicked()
    {
        rightImage.color = new Color(rightImage.color.r, rightImage.color.g, rightImage.color.b, 0f);
        rightText.enabled = false;
    }

    public void ChangeColorClicked()
    {
        changeColorImage.color = new Color(changeColorImage.color.r, changeColorImage.color.g, changeColorImage.color.b, 0f);
        changeColorText.enabled = false;
    }

#endif
}
