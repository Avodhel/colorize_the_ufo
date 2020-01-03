using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour {

    public Animator transitionAnim;
    public GameObject transitionPanel;


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

    public void LoadScene(int scene)
    {
        StartCoroutine(TransitionBetweenScenes(scene));
    }

    private IEnumerator TransitionBetweenScenes(int scene)
    {
        transitionAnim.SetTrigger("transition");
        yield return new WaitUntil(()=>transitionPanel.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(scene);
    }
}
