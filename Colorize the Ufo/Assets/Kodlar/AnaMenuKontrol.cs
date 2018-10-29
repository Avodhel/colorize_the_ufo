using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuKontrol : MonoBehaviour {

    public void oyunaBasla()
    {
        SceneManager.LoadScene(1);
    }

    public void oyundanCik()
    {
        Application.Quit();
    }
}
