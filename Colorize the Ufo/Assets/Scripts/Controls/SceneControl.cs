using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour {

    public Animator transitionAnim;
    public GameObject panel;


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
        StartCoroutine(transitionBetweenScenes(scene));
    }

    private IEnumerator transitionBetweenScenes(int scene)
    {
        transitionAnim.SetTrigger("transition");
        yield return new WaitUntil(()=>panel.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(scene);
    }
}
