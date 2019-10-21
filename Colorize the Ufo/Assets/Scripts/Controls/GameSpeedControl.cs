using UnityEngine;

public class GameSpeedControl : MonoBehaviour {

    float gameSpeedChangeTime = 0f;
	
	private void Update ()
    {
        gameSpeedChange();
    }

    private void gameSpeedChange()
    {
        gameSpeedChangeTime += Time.deltaTime;

        while (gameSpeedChangeTime > 5f) 
        {
            GameControl.gameManager.gameSpeed("increase", 0.02f);
            gameSpeedChangeTime = 0f;
        }
    }
}
