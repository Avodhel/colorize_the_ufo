using UnityEngine;

public class MainMenuControl : MonoBehaviour {

    public void oyunaBasla()
    {
        SceneControl.sceneManager.loadScene(1);
    }

    public void oyundanCik()
    {
        Application.Quit();
    }
}
