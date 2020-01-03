using UnityEngine;

public class MainMenuControl : MonoBehaviour {

    public void OyunaBasla()
    {
        SceneControl.sceneManager.LoadScene(1);
    }

    //public void OyundanCik()
    //{
    //    Application.Quit();
    //}
}
