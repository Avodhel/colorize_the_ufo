using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

    public static SceneControl sceneManager { get; private set; }

    private void Awake()
    {
        if (sceneManager == null)
        {
            sceneManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void loadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}
